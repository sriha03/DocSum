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

        public string ProcessDocument(byte[] documentData, string fileName);
        public List<string> ParseDocument(string filePath);

        public string SummarizeDocument(string filePath);

    }
}
