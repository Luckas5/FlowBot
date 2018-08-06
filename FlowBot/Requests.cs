using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowBot
{
    public static class Requests
    {
        public static string Alterations = "/alterations";
        public static string Service = "/qnamaker";
        public static string GenerateAnswer = "/knowledgebases/" + Keys.KnowledgeBaseId + "/generateAnswer/";
        public static string Knowledgebases = "/knowledgebases/";
    }
}
