using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Sampaio.Shared.Notifications;
using Sampaio.Shared.Persistence;
using Sampaio.Shared.Security;

namespace Sampaio.Domain.Entities
{
    public class User : BaseEntity
    {
        protected User()
        {
        }

        public bool Active { get; private set; }

        public string Email { get; private set; }

        [JsonIgnore] public string Password { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string AvatarUrl { get; private set; }

        [JsonIgnore] public string PasswordRecoverToken { get; private set; }
        [JsonIgnore] public string ActivationToken { get; private set; }
        public ICollection<UserPhone> Phones { get; private set; } = new List<UserPhone>();
        public ICollection<UserDevice> UserDevices { get; private set; } = new List<UserDevice>();

        public string FullName => $"{FirstName} {LastName}".Trim();

        public void SetValidationToke(string token) => ActivationToken = token;
        public static User New(
            string email,
            string password,
            string firstName,
            string lastName = null) => new User
        {
            CreatedAt = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            Active = false,
            Email = email,
            Password = password,
            FirstName = firstName,
            LastName = lastName
        };
        
        public Driver Driver { get; private set; }
        public static User NewDriver(
            Driver driver,
            string email,
            string password,
            string firstName,
            string lastName = null)
        {
            var user = New(email, password, firstName, lastName);
            user.Driver = driver;
            return user;
        }
        public Commerce Commerce { get; private set; }
        public static User NewCommerce(
            Commerce commerce,
            string email,
            string password,
            string firstName,
            string lastName = null)
        {
            var user = New(email, password, firstName, lastName);
            user.Commerce = commerce;
            return user;
        }

        public static User NewBackoffice(BackofficeUser backofficeUser,
            string email,
            string password,
            string firstName,
            string lastName = null)
        {
            var user = New(email, password, firstName, lastName);
            user.BackofficeUser = backofficeUser;
            return user;
        }

        public void Update(
            string firstName, string email,
            string lastName = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public bool ChangeStatus(
            SessionUser loggedUser,
            IDomainNotification notifications)
        {
            if (loggedUser.Id == Id)
            {
                notifications?.Handle("Não é permitido alterar o próprio status");
                return false;
            }

            Active = !Active;
            return true;
        }

        public void NewPasswordRecoverToken(string token) => PasswordRecoverToken = token;

        public void ActivateAccount() => Active = true;
        public void UpdatePasswordWithRecoverToken(string passwordHash) =>
            (PasswordRecoverToken, Password) = (string.Empty, passwordHash);

        public void UpdatePassword(string passwordHash) => Password = passwordHash;

        public bool CanBeRemoved(SessionUser loggedUser,
            IDomainNotification notifications)
        {
            if (loggedUser.Id == Id)
            {
                notifications?.Handle("Não é permitido auto exclusão");
                return false;
            }

            Deleted = true;

            return true;
        }

        public BackofficeUser BackofficeUser { get; private set; }

        public void UpdateAvatarUrl(string avatarUrl) => AvatarUrl = avatarUrl;

        public void UpdateBackofficeUser(BackofficeUser backofficeUser) => BackofficeUser = backofficeUser;
        public void AcceptTerms()
        {
             if (Driver != null)
             {
                 // Driver.AcceptTerms();
             }
             else if (Commerce != null)
             {
                 // Commerce.AcceptTerms();
             }
        }

        public void UpdateCommerce(Commerce commerce) => Commerce = commerce;
        public void UpdateDriver(Driver driver) => Driver = driver;
        public void ClearPhone() => Phones= new List<UserPhone>();
    }
}
