﻿using MongoDB.Bson.Serialization.Attributes;

namespace AtomicChessPuzzles.Models
{
    public class Report
    {
        [BsonElement("_id")]
        public string ID { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("reporter")]
        public string Reporter { get; set; }

        [BsonElement("reported")]
        public string Reported { get; set; }

        [BsonElement("reason")]
        public string Reason { get; set; }

        [BsonElement("reasonExplanation")]
        public string ReasonExplanation { get; set; }

        public Report(string id, string type, string reporter, string reported, string reason, string reasonExplanation)
        {
            ID = id;
            Type = type;
            Reporter = reporter;
            Reported = reported;
            Reason = reason;
            ReasonExplanation = reasonExplanation;
        }
    }
}
