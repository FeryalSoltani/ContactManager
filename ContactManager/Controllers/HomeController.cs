using System.Text.RegularExpressions;
using System.Web.Mvc;
using ContactManager.Models;

namespace ContactManager.Controllers
{
    public class HomeController : Controller
    {
        private IContactManagerRepository _repository;

        public HomeController()
            : this(new EntityContactManagerRepository())
        { }

        public HomeController(IContactManagerRepository repository)
        {
            _repository = repository;
        }

        protected void ValidateContact(ContactInfo contactToValidate)
        {
            if (contactToValidate.FirstName == null || contactToValidate.FirstName.Trim().Length == 0)
                ModelState.AddModelError("FirstName", "First name is required.");
            if (contactToValidate.LastName == null || contactToValidate.LastName.Trim().Length == 0)
                ModelState.AddModelError("LastName", "Last name is required.");
            if (contactToValidate.Number == null || contactToValidate.Number.Trim().Length == 0)
                ModelState.AddModelError("Number", "Number is required.");
            else if (contactToValidate.Number != null)
            {
                var myMatch = System.Text.RegularExpressions.Regex.Match(contactToValidate.Number, "^[0-9]+$");
                if (!myMatch.Success)
                {
                    ModelState.AddModelError("Number", "Invalid phone number.");
                }
            }
            if (contactToValidate.Type == null || contactToValidate.Type.Trim().Length == 0)
                ModelState.AddModelError("Type", "Type is required.(Mobile, Home or Office)");
            if (contactToValidate.Email != null)
            {
                var myMatch = System.Text.RegularExpressions.Regex.Match(contactToValidate.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                if (!myMatch.Success)
                {
                    ModelState.AddModelError("Email", "Invalid email address.");
                }
            }
        }

        public ActionResult Index()
        {
            return View(_repository.ListContacts());
        }

        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "Id")] ContactInfo contactToCreate)
        {
            // Validation logic
            ValidateContact(contactToCreate);
            if (!ModelState.IsValid)
                return View();

            // Database logic
            try
            {
                _repository.CreateContact(contactToCreate);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View(_repository.GetContact(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Number,Type,Email,IsActive")] ContactInfo contactToEdit)
        {
            // Validation logic
            ValidateContact(contactToEdit);
            if (!ModelState.IsValid)
                return View();

            // Database logic
            try
            {
                _repository.EditContact(contactToEdit);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View(_repository.GetContact(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(ContactInfo contactToDelete)
        {
            try
            {
                _repository.DeleteContact(contactToDelete);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
