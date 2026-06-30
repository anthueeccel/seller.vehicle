using Microsoft.AspNetCore.Mvc;
using mvc_client.Models;

public class ClientController : Controller
{
    private readonly ClientRepository _repository;
    private readonly CountryPhoneCodeService _countryPhoneCodeService;
    private readonly UserRepository _userRepository;

    public ClientController(ClientRepository repository, CountryPhoneCodeService countryPhoneCodeService, UserRepository userRepository)
    {
        _repository = repository;
        _countryPhoneCodeService = countryPhoneCodeService;
        _userRepository = userRepository;
    }

    public IActionResult Index()
    {
        var clients = _repository.GetAll();
        var users = _userRepository.GetAll();

        var viewModels = clients.Select(client => new ClientWithRoleViewModel
        {
            Client = client,
            Role = users.FirstOrDefault(u => u.Client.Id == client.Id)?.Role
        }).ToList();

        return View(viewModels);
    }

    public IActionResult Details(int id)
    {
        var client = _repository.Get(id);

        if (client == null)
            return NotFound();

        var user = _userRepository.GetAll().FirstOrDefault(u => u.Client.Id == client.Id);

        var viewModel = new ClientWithRoleViewModel
        {
            Client = client,
            Role = user?.Role
        };

        return View(viewModel);
    }

    public IActionResult Create()
    {
        ViewBag.CountryPhoneCodes = _countryPhoneCodeService.GetAll();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Client client, string? countryPhoneCode)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.CountryPhoneCodes = _countryPhoneCodeService.GetAll();
            return View(client);
        }

        client.CountryPhoneCode = countryPhoneCode;

        _repository.Save(client);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var client = _repository.Get(id);

        if (client == null)
            return NotFound();

        ViewBag.CountryPhoneCodes = _countryPhoneCodeService.GetAll();
        return View(client);
    }

    [HttpPost]
    public IActionResult Edit(Client client, string? countryPhoneCode)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.CountryPhoneCodes = _countryPhoneCodeService.GetAll();
            return View(client);
        }

        client.CountryPhoneCode = countryPhoneCode;

        _repository.Update(client);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var client = _repository.Get(id);

        if (client == null)
            return NotFound();

        var user = _userRepository.GetAll().FirstOrDefault(u => u.Client.Id == client.Id);

        var viewModel = new ClientWithRoleViewModel
        {
            Client = client,
            Role = user?.Role
        };

        return View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        _repository.Delete(id);

        return RedirectToAction(nameof(Index));
    }
}