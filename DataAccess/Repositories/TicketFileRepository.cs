using DataAccess.Interface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class TicketFileRepository : ITicketRepository
    {
        private string _path;
        public TicketFileRepository(string path)
        {
            _path = path;

            if (!File.Exists(path))
                using (var file = File.Create(path))
                {
                    file.Close();
                }

        }

        public void Book(Ticket t)
        {
            var list = GetTickets().ToList();

            bool found = false;

            do
            {
                found = list.Any(x => x.Id == t.Id);
            } while (found);

            list.Add(t);

            string contents = JsonSerializer.Serialize(list);

            File.WriteAllText(_path, contents);
        }


        public IQueryable<Ticket> GetTickets()
        {

            string contents = File.ReadAllText(_path);
            if (string.IsNullOrEmpty(contents))
            {
                return new List<Ticket>().AsQueryable();
            }
            try
            {
                var list = JsonSerializer.Deserialize<List<Ticket>>(contents);
                return list.AsQueryable();
            }
            catch (Exception ex)
            {
                throw new Exception("Badly formatted data was read");
            }
        }
    }
}
