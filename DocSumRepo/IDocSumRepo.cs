using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using common.model;

namespace DocSumRepository
{
    public interface IDocSumRepo
    {
        public string SaveDocument(byte[] documentData, string fileName);
        public void ParseDocument(string filePath);
        public Task StoreConversation(List<string> pages, List<string> summaries,string filePath);
    }

}
