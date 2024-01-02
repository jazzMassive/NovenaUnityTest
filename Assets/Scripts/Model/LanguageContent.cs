using System;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    [Serializable]
    public class LanguageContent
    {
        public string languageId;
        public string languageName;
        public List<Topic> topics;


        public Topic GetTopicById(string id)
        {
            return topics.Find(u => u.topicId == id);
        }
    }
}
