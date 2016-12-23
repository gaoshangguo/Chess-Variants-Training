﻿using ChessVariantsTraining.DbRepositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessVariantsTraining.Models.Variant960
{
    public class LobbySeek
    {
        public string ID { get; set; }
        public int SecondsInitial { get; private set; }
        public int SecondsIncrement { get; private set; }
        public string Variant { get; private set; }
        public bool Symmetrical { get; private set; }
        public int? Owner { get; private set; }
        public DateTime LatestBump { get; set; }
        public string ClientID { get; private set; }

        public LobbySeek(int secondsInitial, int secondsIncrement, string variant, bool symmetrical, int? owner, string clientId)
        {
            SecondsInitial = secondsInitial;
            SecondsIncrement = secondsIncrement;
            Variant = variant;
            Symmetrical = symmetrical;
            Owner = owner;
            ClientID = clientId;
        }

        public static bool TryParse(string encoded, int? owner, string clientId, out LobbySeek seek)
        {
            string[] parts = encoded.Split(';');
            if (parts.Length != 4)
            {
                seek = null;
                return false;
            }

            int secondsInitial;
            if (!int.TryParse(parts[0], out secondsInitial))
            {
                seek = null;
                return false;
            }
            if ((secondsInitial != 30 && secondsInitial != 45 && secondsInitial != 90) && (secondsInitial % 60 != 0 || secondsInitial > 30 * 60 || secondsInitial < 0))
            {
                seek = null;
                return false;
            }

            int secondsIncrement;
            if (!int.TryParse(parts[1], out secondsIncrement))
            {
                seek = null;
                return false;
            }

            if (secondsIncrement < 0 || secondsIncrement > 30 || (secondsIncrement == 0 && secondsInitial == 0))
            {
                seek = null;
                return false;
            }

            string[] allowedVariants = new string[] { "Antichess", "Atomic", "Horde", "KingOfTheHill", "RacingKings", "ThreeCheck" };
            string variant = parts[2];
            if (!allowedVariants.Contains(parts[2]))
            {
                seek = null;
                return false;
            }

            if (parts[3] != "true" && parts[3] != "false")
            {
                seek = null;
                return false;
            }
            bool symmetrical = parts[3] == "true";

            seek = new LobbySeek(secondsInitial, secondsIncrement, variant, symmetrical, owner, clientId);
            return true;
        }

        public string SeekJson(IUserRepository userRepository)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("v", Variant);
            data.Add("s", Symmetrical ? "Symmetrical" : "Asymmetrical");
            if (Owner.HasValue)
            {
                data.Add("o", userRepository.FindById(Owner.Value).Username);
            }
            else
            {
                data.Add("o", "(Anonymous)");
            }

            string minutes;
            switch (SecondsInitial)
            {
                case 30:
                    minutes = "½";
                    break;
                case 45:
                    minutes = "¾";
                    break;
                case 90:
                    minutes = "1.5";
                    break;
                default:
                    minutes = (SecondsInitial / 60).ToString();
                    break;
            }
            data.Add("c", string.Concat(minutes, "+", SecondsIncrement));
            data.Add("i", ID);
            return JsonConvert.SerializeObject(data);
        }
    }
}
