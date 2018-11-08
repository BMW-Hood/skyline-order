using Infrastructure;
using Models;
using Repositories;
using System;
using WebAPI.DataContracts.Requests;

namespace WebAPI.Services
{
    public interface IUserService
    {
        void Add();

        bool Register(RegisterRequest request);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Add()
        {
            User user = new User
            {
                Birthday = DateTime.Now,
                Email = "18351801922@qq.com",
                Password = EncryptHelper.Md5("123456"),
                Phone = "18351801922"
            };
            _userRepository.Add(user);
            _userRepository.Commit();
        }

        public bool Register(RegisterRequest request)
        {
            //判断用户有没有注册过
            var result = _userRepository.FindByPhoneAndPassword(request.Phone, request.EnCodePassword);
            if (null != result)
            {
                //用户已经注册过了
                return false;
            }
            else
            {
                //用户注册
                var user = new User()
                {
                    Phone = request.Phone,
                    Email = request.Email,
                    Password = request.EnCodePassword,
                    Birthday = request.Birthday,
                };

                _userRepository.Add(user);
                _userRepository.Commit();
                return true;
            }
        }
    }
}