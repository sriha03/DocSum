﻿using com.sun.jndi.dns;
using common.model;
using Microsoft.Azure.Cosmos;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Internal;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocSumRepository
{
    public class DocSumRepo : IDocSumRepo
    {
        private readonly CosmosClient _cosmosClient;
        private const string DatabaseId = "DocSum";
        private const string ContainerId = "Conversation";
        private readonly string _storagePath;
     
        public DocSumRepo(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
            _storagePath = "\\uploads";
        }
        public async Task<List<ConversationModel>> GetConversations()
        {
            var container = _cosmosClient.GetContainer(DatabaseId, ContainerId);

            var query = "SELECT * FROM c";
            var queryDefinition = new QueryDefinition(query);
            var conversations = new List<ConversationModel>();

            var resultSetIterator = container.GetItemQueryIterator<ConversationModel>(queryDefinition);

            while (resultSetIterator.HasMoreResults)
            {
                var currentResultSet = await resultSetIterator.ReadNextAsync();
                conversations.AddRange(currentResultSet);
            }

            return conversations;
        }

        public string SaveDocument(byte[] documentData, string fileName)
        {
            var filePath = Path.Combine(_storagePath, fileName);
            File.WriteAllBytes(filePath, documentData);
            return filePath;
        }
        public void ParseDocument(string filePath)
        {
            File.ReadAllBytes(filePath);
        }


        public async Task StoreConversation(List<string> pages, List<string> summaries,string filePath)
        {
            var container = _cosmosClient.GetContainer(DatabaseId, ContainerId);

            var conversation = new ConversationModel    
            {
                id= Guid.NewGuid().ToString(),
                ConKey = Guid.NewGuid().ToString(),
                Pages = pages,
                Summaries = summaries,
                DocUrl = filePath,
                Conv = ""
            };

            var response = await container.CreateItemAsync(conversation);
        }






       

      /*  public async Task<List<Student>> getstudentbyid(int id)
        {
            var container = _cosmosClient.GetContainer(DatabaseId, ContainerId);

            var query = "SELECT * FROM c WHERE c.sno = @StudentId";
            var queryDefinition = new QueryDefinition(query).WithParameter("@StudentId", id);
            var students = new List<Student>();

            var resultSetIterator = container.GetItemQueryIterator<Student>(queryDefinition);

            while (resultSetIterator.HasMoreResults)
            {
                var currentResultSet = await resultSetIterator.ReadNextAsync();
                students.AddRange(currentResultSet);
            }

            return students;
        }*/

    }

}
