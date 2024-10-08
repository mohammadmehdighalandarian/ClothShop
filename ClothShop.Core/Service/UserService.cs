﻿using ClothShop.Core.Convertors;
using ClothShop.Core.DTOs.User;
using ClothShop.Core.Generator;
using ClothShop.Core.Security;
using ClothShop.Core.Service.Interface;
using ClothShop.DataLayer.Context;
using ClothShop.DataLayer.Entities.User;
using ClothShop.DataLayer.Entities.Wallet;
using Microsoft.EntityFrameworkCore;

namespace ClothShop.Core.Service;

public class UserService:IUserService
{
    private readonly ShopContext _context;

    public UserService(ShopContext shopContext)
    {
        _context = shopContext;
    }

    public bool IsExistUserName(string userName)
    {
        return _context.Users.Any(u => u.UserName == userName);
    }

    public bool IsExistEmail(string email)
    {
        return _context.Users.Any(u => u.Email == email);
    }

    public int AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user.UserId;
    }

    public User LoginUser(LoginViewModel login)
    {
        string hashPassword = PasswordHelper.EncodePasswordMd5(login.Password);
        string email = FixedText.FixEmail(login.Email);
        return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == hashPassword);
    }

    public User GetUserByEmail(string email)
    {
        return _context.Users.SingleOrDefault(u => u.Email == email);
    }

    public User GetUserById(int userId)
    {
        return _context.Users.Find(userId);
    }

    public User GetUserByActiveCode(string activeCode)
    {
        return _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
    }

    public User GetUserByUserName(string username)
    {
        return _context
                .Users
                .SingleOrDefault(u => u.UserName == username);
    }

    public void UpdateUser(User user)
    {
        _context.Update(user);
        _context.SaveChanges();
    }

    public bool ActiveAccount(string activeCode)
    {
        var user = _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
        if (user == null || user.IsActive)
            return false;

        user.IsActive = true;
        user.ActiveCode = NameGenerator.GenerateUniqCode();
        _context.SaveChanges();

        return true;
    }

    public int GetUserIdByUserName(string userName)
    {
        return _context.Users.Single(u => u.UserName == userName).UserId;
    }

    public void DeleteUser(int userId)
    {
        User user = GetUserById(userId);
        user.IsDelete = true;
        UpdateUser(user);
    }

    public void ActiveUser(int userId)
    {
        User user = GetUserById(userId);
        user.IsDelete = false;
        UpdateUser(user);
    }

    public InformationUserViewModel GetUserInformation(string username)
    {
        var user = GetUserByUserName(username);
        var address = GetActiveAddressByUserId(user.UserId);
        InformationUserViewModel information = new InformationUserViewModel();
        information.UserName = user.UserName;
        information.Email = user.Email;
        information.RegisterDate = user.RegistertionDate;
        information.Wallet = BalanceUserWallet(username);
        if (address is null)
        {
            information.Address = "نامشخص";
        }
        else
        {
            information.Address = $"{address.Province}|{address.City}|{address.Neighborhood}|پلاك:{address.Plate}واحد:{address.ApartmentNo}";
        }

        return information;

    }

    public InformationUserViewModel GetUserInformation(int userId)
    {
        var user = GetUserById(userId);
        
        InformationUserViewModel information = new InformationUserViewModel();
        information.UserName = user.UserName;
        information.Email = user.Email;
        information.RegisterDate = user.RegistertionDate;
        information.Wallet = BalanceUserWallet(user.UserName);
        

        return information;
    }

    public SideBarUserPanelViewModel GetSideBarUserPanelData(string username)
    {
        return _context.Users.Where(u => u.UserName == username).Select(u => new SideBarUserPanelViewModel()
        {
            UserName = u.UserName,
            ImageName = u.UserAvatar,
            RegisterDate = u.RegistertionDate
        }).Single();
    }

    public EditProfileViewModel GetDataForEditProfileUser(string username)
    {
        var User = GetUserByUserName(username);
        var address = GetActiveAddressByUserId(User.UserId); 

        EditProfileViewModel EditUser=new EditProfileViewModel()
        {
            AvatarName = User.UserAvatar,
            Email = User.Email,
            UserName = User.UserName,
            Province = address.Province,            
            City = address.City,                   
            Neighborhood = address.Neighborhood,    
            ApartmentNo = address.ApartmentNo,      
            Plate = address.Plate,                  
            PostCode = address.PostCode,            
            RecieverFName = address.RecieverFName,  
            RecieverLName = address.RecieverLName,  
            RecieverPhoneNo = address.RecieverPhoneNo
        };

        return EditUser;

    }

    public void EditProfile(string username, EditProfileViewModel profile)
    {
        if (profile.UserAvatar != null)
        {
            string imagePath = "";
            if (profile.AvatarName != "Defult.jpg")
            {
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.AvatarName);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }

            profile.AvatarName = NameGenerator.GenerateUniqCode() + Path.GetExtension(profile.UserAvatar.FileName);
            imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.AvatarName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                profile.UserAvatar.CopyTo(stream);
            }

        }

        var user = GetUserByUserName(username);
        user.UserName = profile.UserName;
        user.Email = profile.Email;
        user.UserAvatar = profile.AvatarName;

        UpdateUser(user);

    }

    public bool CompareOldPassword(string oldPassword, string username)
    {
        string hashOldPassword = PasswordHelper.EncodePasswordMd5(oldPassword);
        return _context.Users.Any(u => u.UserName == username && u.Password == hashOldPassword);
    }

    public List<Address> GetAddressByUserId(int userId)
    {
        return _context
            .Addresses
            .Where(x => x.UserId == userId)
            .ToList();
    }

    public Address GetActiveAddressByUserId(int userId)
    {
        return _context.Addresses.SingleOrDefault(x => x.UserId == userId && x.IsActive == true);
    }

    public void ChangeUserPassword(string userName, string newPassword)
    {
        var user = GetUserByUserName(userName);
        user.Password = PasswordHelper.EncodePasswordMd5(newPassword);
        UpdateUser(user);
    }

    public void AddNewAddress(Address address, int userId)
    {
        address.UserId=userId;
        _context.Add(address);
        _context.SaveChanges();
    }

    public int BalanceUserWallet(string userName)
    {

        int userId = GetUserIdByUserName(userName);

        var enter = _context.Wallets
            .Where(w => w.UserId == userId && w.TypeId == 1 && w.IsPay)
            .Select(w => w.Amount).ToList();

        var exit = _context.Wallets
            .Where(w => w.UserId == userId && w.TypeId == 2)
            .Select(w => w.Amount).ToList();

        return (enter.Sum() - exit.Sum());
    }

    public List<WalletViewModel> GetWalletUser(string userName)
    {
        int userId = GetUserIdByUserName(userName);

        return _context.Wallets
            .Where(w => w.IsPay && w.UserId == userId)
            .Select(w => new WalletViewModel()
            {
                Amount = w.Amount,
                DateTime = w.CreateDate,
                Description = w.Description,
                Type = w.TypeId
            })
            .ToList();
    }

    public int ChargeWallet(string userName, int amount, string description, bool isPay = false)
    {
        Wallet wallet = new Wallet()
        {
            Amount = amount,
            CreateDate = DateTime.Now,
            Description = description,
            IsPay = isPay,
            TypeId = 1,
            UserId = GetUserIdByUserName(userName)
        };
        return AddWallet(wallet);
    }

    public int AddWallet(Wallet wallet)
    {
        _context.Wallets.Add(wallet);
        _context.SaveChanges();
        return wallet.WalletId;
    }

    public Wallet GetWalletByWalletId(int walletId)
    {
        return _context.Wallets.Find(walletId);
    }

    public void UpdateWallet(Wallet wallet)
    {
        _context.Wallets.Update(wallet);
        _context.SaveChanges();
    }

    public UserForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
    {
        IQueryable<User> result = _context.Users;

        result = result.Where(x => x.IsDelete == false);

        if (!string.IsNullOrEmpty(filterEmail))
        {
            result = result.Where(u => u.Email.Contains(filterEmail));
        }

        if (!string.IsNullOrEmpty(filterUserName))
        {
            result = result.Where(u => u.UserName.Contains(filterUserName));
        }

        // Show Item In Page
        int take = 20;
        int skip = (pageId - 1) * take;


        UserForAdminViewModel list = new UserForAdminViewModel();
        list.CurrentPage = pageId;
        list.PageCount = result.Count() / take;
        list.Users = result.OrderBy(u => u.RegistertionDate).Skip(skip).Take(take).ToList();

        return list;
    }

    public UserForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
    {
        IQueryable<User> result = _context.Users.IgnoreQueryFilters().Where(u => u.IsDelete);

        if (!string.IsNullOrEmpty(filterEmail))
        {
            result = result.Where(u => u.Email.Contains(filterEmail));
        }

        if (!string.IsNullOrEmpty(filterUserName))
        {
            result = result.Where(u => u.UserName.Contains(filterUserName));
        }

        // Show Item In Page
        int take = 20;
        int skip = (pageId - 1) * take;


        UserForAdminViewModel list = new UserForAdminViewModel();
        list.CurrentPage = pageId;
        list.PageCount = result.Count() / take;
        list.Users = result.OrderBy(u => u.RegistertionDate).Skip(skip).Take(take).ToList();

        return list;
    }

    public int AddUserFromAdmin(CreateUserViewModel user)
    {
        User addUser = new User();
        addUser.Password = PasswordHelper.EncodePasswordMd5(user.Password);
        addUser.ActiveCode = NameGenerator.GenerateUniqCode();
        addUser.Email = user.Email;
        addUser.IsActive = true;
        addUser.RegistertionDate = DateTime.Now;
        addUser.UserName = user.UserName;

        #region Save Avatar

        if (user.UserAvatar != null)
        {
            string imagePath = "";
            addUser.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(user.UserAvatar.FileName);
            imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", addUser.UserAvatar);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                user.UserAvatar.CopyTo(stream);
            }
        }

        #endregion

        return AddUser(addUser);

    }

    public int ChangeAddress(int addressId)
    {
        // Fetch the address with the specified ID, including the user to avoid a second database call
        var address = _context.Addresses.SingleOrDefault(a => a.Id == addressId);

        // Handle case where address is not found
        if (address == null)
        {
            throw new ArgumentException("Address not found", nameof(addressId));
        }

        // Find the currently active address for the same user
        var activeAddress = _context.Addresses
            .SingleOrDefault(x => x.UserId == address.UserId && x.IsActive);

        // If the address is already active, return the UserId without making changes
        if (address.IsActive)
        {
            return address.UserId;
        }

        // Use a transaction to ensure both updates are applied atomically
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                // Set the new address as active
                address.IsActive = true;
                _context.Addresses.Update(address);

                // If an active address was found, set it as inactive
                if (activeAddress != null)
                {
                    activeAddress.IsActive = false;
                    _context.Addresses.Update(activeAddress);
                }

                // Save changes to the database
                _context.SaveChanges();

                // Commit the transaction
                transaction.Commit();
            }
            catch (Exception)
            {
                // Rollback the transaction if something goes wrong
                transaction.Rollback();
                throw; // Re-throw the exception to be handled by the caller
            }
        }

        return address.UserId;
    }

    public EditUserViewModel GetUserForShowInEditMode(int userId)
    {
        return _context.Users.Where(u => u.UserId == userId)
            .Select(u => new EditUserViewModel()
            {
                UserId = u.UserId,
                AvatarName = u.UserAvatar,
                Email = u.Email,
                UserName = u.UserName,
                Password = u.Password,
                UserRoles = u.UserRoles.Select(r => r.RoleId).ToList()
            }).Single();
    }

    public void EditUserFromAdmin(EditUserViewModel editUser)
    {
        User user = GetUserById(editUser.UserId);
        user.Email = editUser.Email;
        if (!string.IsNullOrEmpty(editUser.Password))
        {
            user.Password = PasswordHelper.EncodePasswordMd5(editUser.Password);
        }

        if (editUser.UserAvatar != null)
        {
            //Delete old Image
            if (editUser.AvatarName != "Defult.jpg")
            {
                string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", editUser.AvatarName);
                if (File.Exists(deletePath))
                {
                    File.Delete(deletePath);
                }
            }

            //Save New Image
            user.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(editUser.UserAvatar.FileName);
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", user.UserAvatar);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                editUser.UserAvatar.CopyTo(stream);
            }
        }

        _context.Users.Update(user);
        _context.SaveChanges();
    }
}