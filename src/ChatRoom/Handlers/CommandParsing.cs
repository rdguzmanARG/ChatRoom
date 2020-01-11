using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Handlers
{
    public static class CommandParsing
    {
        public static string ParseMessage(string message)
        {
            if (message.ToLower().StartsWith("/stock="))
            {
                return QuotesParsing.GetQuote(message.Replace("/stock=", ""));
            }

            return "";
        }

    }
}
