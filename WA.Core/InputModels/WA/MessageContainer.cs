using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Core.InputModels.WA
{
    public class Change
    {
        public Value value { get; set; }
        public string field { get; set; }
    }

    public class Conversation
    {
        public string id { get; set; }
        public Origin origin { get; set; }
    }

    public class Origin
    {
        public string type { get; set; }
    }

    public class Contact
    {
        public Profile profile { get; set; }
        public string wa_id { get; set; }
    }

    public class Entry
    {
        public string id { get; set; }
        public List<Change> changes { get; set; }
    }

    public class Message
    {
        public string from { get; set; }
        public string id { get; set; }
        public string timestamp { get; set; }
        public Text text { get; set; }
        public string type { get; set; }

        public Context context { get; set; }

        public Button button { get; set; }
    }

    public class Context
    {
        public string from { get; set; }

        public string id { get; set; }
    }

    public class Button
    {
        public string payload { get; set; }

        public string text { get; set; }
    }

    public class Metadata
    {
        public string display_phone_number { get; set; }
        public string phone_number_id { get; set; }
    }

    public class Pricing
    {
        public bool billable { get; set; }
        public string pricing_model { get; set; }
        public string category { get; set; }
    }

    public class Status
    {
        public string id { get; set; }
        public string status { get; set; }
        public string timestamp { get; set; }
        public string recipient_id { get; set; }
        public Conversation conversation { get; set; }
        public Pricing pricing { get; set; }
    }

    public class Profile
    {
        public string name { get; set; }
    }

    public class Root
    {
        public string @object { get; set; }
        public List<Entry> entry { get; set; }
    }

    public class Text
    {
        public string body { get; set; }
    }

    public class Value
    {
        public string messaging_product { get; set; }
        public Metadata metadata { get; set; }
        public List<Contact> contacts { get; set; }
        public List<Message> messages { get; set; }
        public List<Status> statuses { get; set; }
    }

}
