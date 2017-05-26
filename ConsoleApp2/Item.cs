namespace ConsoleApp2
{
    public class Item
    {
        string description;
        public Item()
        {
            description = "";
        }
        public Item(string str)
        {
            description = str;
        }
        public string GetDescription()
        {
            return description;
        }
    }
}
