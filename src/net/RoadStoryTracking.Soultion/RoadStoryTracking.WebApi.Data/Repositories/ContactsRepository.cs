using Microsoft.EntityFrameworkCore;
using RoadStoryTracking.WebApi.Data.Context;
using RoadStoryTracking.WebApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoadStoryTracking.WebApi.Data.Repositories
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly RoadStoryTrackingDbContext _dbContext;

        public ContactsRepository(RoadStoryTrackingDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), $"{nameof(RoadStoryTrackingDbContext)} cannot be null!");
        }

        public Contact AddContact(Contact friend)
        {
            _dbContext.Contacts.Add(friend);
            _dbContext.SaveChanges();

            return friend;
        }

        public Contact DeleteContact(Contact contact)
        {
            _dbContext.Contacts.Remove(contact);
            _dbContext.SaveChanges();

            return contact;
        }

        public List<Contact> GetAcceptedContacts(string userId)
        {
            return _dbContext.Contacts
                .Include(c => c.RequestedBy)
                .Include(c => c.RequestedTo)
                .Where(c => c.Status == ContactStatuses.Accepted)
                .Where(c => c.RequestedById == userId || c.RequestedToId == userId)
                .ToList();
        }

        public List<Contact> GetAllContacts(string userId)
        {
            return _dbContext.Contacts
                .Include(c => c.RequestedBy)
                .Include(c => c.RequestedTo)
                .Where(c => c.RequestedById == userId || c.RequestedToId == userId)
                .ToList();
        }

        public Contact GetContact(Guid contactId)
        {
            return _dbContext.Contacts
                .Include(c => c.RequestedBy)
                .Include(c => c.RequestedTo)
                .FirstOrDefault(c => c.Id == contactId);
        }

        public List<ApplicationUser> GetPotentionalContacts(string userId, string userName)
        {
            var contacts = GetAllContacts(userId);

            var users = _dbContext.Users
                .Where(u => u.UserName.Contains(userName))
                .ToList()
                .Where(u => !contacts.Any(c => c.RequestedById == u.Id))
                .Where(u => !contacts.Any(c => c.RequestedToId == u.Id))
                .Where(u => u.Id != userId)
                .Take(10)
                .ToList();

            return users;
        }

        public Contact UpdateContact(Contact contact)
        {
            _dbContext.SaveChanges();

            return contact;
        }
    }
}