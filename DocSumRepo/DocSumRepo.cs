using common.model;
using Microsoft.Azure.Cosmos;
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

        private readonly string _storagePath;

        public DocSumRepo()
        {
            _storagePath = "\\uploads";
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

        /*
        private readonly CosmosClient _cosmosclient;
        private const string DatabaseId = "StudentDB";
        private const string ContainerId = "Student";
        public DocSumRepo(CosmosClient cosmosClient)
        {
            _cosmosclient = cosmosClient;
        }
        public async Task<List<conversations>> GetConversations()
        {
            var container = _cosmosclient.GetContainer(DatabaseId, ContainerId);

            var query = "SELECT * FROM c";
            var queryDefinition = new QueryDefinition(query);
            var conversations = new List<conversations>();

            var resultSetIterator = container.GetItemQueryIterator<conversations>(queryDefinition);

            while (resultSetIterator.HasMoreResults)
            {
                var currentResultSet = await resultSetIterator.ReadNextAsync();
                conversations.AddRange(currentResultSet);
            }

            return conversations;
        }
        
        public async Task<List<Student>> getstudentbyid(int id)
        {
            var container = _cosmosclient.GetContainer(DatabaseId, ContainerId);

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
        }
        */
    }

}
