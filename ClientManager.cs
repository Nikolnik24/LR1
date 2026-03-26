using ClientSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagerSpace
{
    public class ClientManager
    {
        public List<Client> Clients { get; private set; }
        public ClientManager()
        {
            Clients = new List<Client>();
            LoadClients();
        }
        public void AddClient(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            Clients.Add(client);
            SaveClients();
        }
        public void RemoveClient(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            Clients.Remove(client);
            SaveClients();
        }
        public List<Client> SearchClients(string query)
        {
            return Clients.Where(c => c.Name.Contains(query) || c.Email.Contains(query) ||
            c.Phone.Contains(query) || c.Address.Contains(query)).ToList();
        }
        private void SaveClients()
        {
            File.WriteAllLines("clients.txt", Clients.Select(c =>
            $"{c.Name}|{c.Email}|{c.Phone}|{c.Address}"));
        }
        private void LoadClients()
        {
            if (File.Exists("clients.txt"))
            {
                var lines = File.ReadAllLines("clients.txt");
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 4)
                    {
                        Clients.Add(new Client(parts[0], parts[1], parts[2], parts[3]));
                    }
                }
            }
        }
    }
}
