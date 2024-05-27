using MicroService.Domain.Class;
using MicroService.Domain.Repository;
using MicroService.Shared.ApiClient;
using MicroService.Shared.Request.User;

namespace MicroService.UserApi.Manager
{
    public class UserManager
    {
        private readonly IRepository<User> _userRepository;
        private readonly GatewayApiClient _gatewayApiClient;
        public UserManager(IRepository<User> userRepository, GatewayApiClient gatewayApiClient)
        {
            _userRepository = userRepository;
            _gatewayApiClient = gatewayApiClient;
        }

        public User GetUser(Guid guid)
        {
            return _userRepository.GetByGuid(guid);
        }
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User Add(CreateUserRequest request)
        {
            var newUser = new User
            {
                Guid = Guid.NewGuid(),
                Name = request.Name,
                TelegramChatId = request.TelegramChatId,
                Balance = 100,

            };
            _userRepository.Add(newUser);
            return newUser;
        }

        public User Update(UpdateUserRequest request)
        {
            var user = _userRepository.GetByGuid(request.Guid);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.Name = request.Name;
            user.TelegramChatId = request.TelegramChatId;
            _userRepository.Update(user);
            return user;
        }

        public void Delete(Guid guid)
        {
            var user = _userRepository.GetByGuid(guid);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            _userRepository.Delete(user);
        }


        public User AddMoney(AddMoneyToUserRequest request)
        {
            var user = _userRepository.GetByGuid(request.Guid);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            user.Balance += request.Amount;
            _userRepository.Update(user);
            return user;
        }



        public Transaction Buy(BuyRequest request)
        {
            var user = _userRepository.GetByGuid(request.UserGuid);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            var item = _gatewayApiClient.GetItem(new Shared.Request.Item.GetItemRequest() { Guid = request.ItemGuid });
            if (item == null)
            {
                throw new Exception("Item not found.");
            }
            // check item amount and buy request amount
            if (item.Amount < request.Amount)
            {
                throw new Exception("Not enough item amount.");
            }

            var total = item.Price * request.Amount;
            if (user.Balance < total)
            {
                throw new Exception("Not enough money.");
            }


            var success = _gatewayApiClient.TriggerBuyItem(request);
            if (!success)
            {
                throw new Exception("Error while buying item.");
            }

            user.Balance -= total;
            _userRepository.Update(user);

            var result = new Transaction
            {
                Guid = Guid.NewGuid(),
                UserGuid = user.Guid,
                Amount = total,
                Date = DateTime.Now
            };

            try
            {
                var message = "";
                message += $" Hello, {user.Name}! \n";
                message += $" You bought {request.Amount} x {item.Name}.\n";
                message += $" Your total is {result.Amount}.\n";
                message += $" Your balance is {user.Balance}.\n";
                message += $" Date: {result.Date}.\n";
                _gatewayApiClient.NotifyUser(new Shared.Request.Notify.NotifyUserRequest { Guid = user.Guid, Message = message });
            }
            catch { }

            return result;
        }




    }
}
