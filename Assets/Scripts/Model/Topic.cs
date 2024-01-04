using System;
using System.Collections.Generic;

namespace Assets.Scripts.Model

{
    [Serializable]
    public class Topic
    {
        public string TopicId;
        public string Name;
        public string Type;
        public List<Media> Media;
    }
}