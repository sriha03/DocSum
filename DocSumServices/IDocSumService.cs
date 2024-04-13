using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using common.model;

namespace DocSumServices
{
    public interface IDocSumService
    {
        /*public Task<List<conversations>> GetConversations();*/

         Task<List<string>> ProcessDocument(byte[] documentData, string fileName) ;
        Task<List<string>> ParseDocument(string filePath);

        Task<List<string>> SummarizeDocument(string filePath);

    }
}
