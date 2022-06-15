using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PracticeApp.Models;
using PracticeApp.Repositories;

namespace PracticeApp.Controllers
{
    public class UsersController : Controller
    {
        private UserRepository UserRepository { get; } = new UserRepository();

        public IActionResult Index()
        {
            return View(UserRepository.Get());
        }

        public ViewResult Edit(int? id)
        {
            ViewBag.UserRoles = new SelectList(UserRepository.GetUserRoles(), "Id", "Name");

            return View(id == null ? new User() : UserRepository.Get(id.Value));
        }

        [HttpPost]
        public ApiResult Edit(User user)
        {
            UserRepository.Add(user);

            return new ApiResult() { IsSuccess = true, Href = Url.Action(nameof(Index), "Users") };
        }

        [HttpPut]
        [ActionName(nameof(Edit))]
        public ApiResult EditPut(User user)
        {
            // TODO: реализовать обновление пользователя
            UserRepository.Update(user);

            return new ApiResult() { IsSuccess = true, Href = Url.Action(nameof(Index), "Users") };
        }

        [HttpDelete]
        public ApiResult Delete(int id)
        {
            // TODO: реализовать удаление
            UserRepository.Delete(id);

            return new ApiResult() { IsSuccess = true, Href = Url.Action(nameof(Index), "Users") };
        }
    }
}
