﻿
namespace Unitester_Domain.Exceptions.Users
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException()
        {
            this.TitleMessage = "Foydalanuvchi topilmadi!";
        }
    }
}
