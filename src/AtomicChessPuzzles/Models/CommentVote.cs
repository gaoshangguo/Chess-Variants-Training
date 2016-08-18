﻿using MongoDB.Bson.Serialization.Attributes;

namespace AtomicChessPuzzles.Models
{
    public class CommentVote
    {
        [BsonElement("_id")]
        public string ID { get; private set; }

        [BsonElement("type")]
        public VoteType Type { get; set; }

        [BsonElement("voter")]
        public int Voter { get; set; }

        [BsonElement("affectedComment")]
        public int AffectedComment { get; set; }

        public CommentVote(VoteType type, int voter, int affectedComment)
        {
            ID = voter + ":" + affectedComment;
            Type = type;
            Voter = voter;
            AffectedComment = affectedComment;
        }
    }
}
