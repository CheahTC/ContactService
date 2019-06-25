namespace ContactService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ContactService.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ContactService.Models.ContactServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ContactService.Models.ContactServiceContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.ContactPersons.AddOrUpdate(
              p => p.Id,
              new ContactPerson { Id = 1, Name = "Benny Fishery" },
              new ContactPerson { Id = 2, Name = "Anne o'Nimus" },
              new ContactPerson { Id = 3, Name = "Paul Enta" }
            );

            context.ContactNumbers.AddOrUpdate(
              x => x.Id,
              new ContactNumber() { Id = 1, Number = "0234567890", ContactPersonId = 1, Active = false },
              new ContactNumber() { Id = 2, Number = "0412345678", ContactPersonId = 2, Active = false },
              new ContactNumber() { Id = 3, Number = "0345678901", ContactPersonId = 3, Active = false }
            );
        }
    }
}
