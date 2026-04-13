namespace Sampaio.Data
{
    public class SeedDevelopment
    {
        // private readonly DataContext _context;
        //
        //
        // private readonly List<InstitutionType> _institutionTypes;
        //
        // private readonly Institution _institution;
        //
        // private readonly Industry _industry;
        //
        // private readonly User _userAuthorizer;
        //
        // private readonly User _userInstitution;
        //
        // private readonly User _userIndustry;
        //
        // private readonly User _userDoctor;
        //
        // public SeedDevelopment(DataContext context,
        //     IPasswordHasherService passwordHasherService)
        // {
        //     _context = context;
        //     
        //     var password = passwordHasherService.Hash("123456");
        //     
        //     _institutionTypes = new List<InstitutionType>
        //     {
        //         new InstitutionType(Guid.Parse("b85b6798-27ff-481f-1463-08d5fc6a9220"), DateTime.UtcNow, true, false, "Hospital"),
        //         new InstitutionType(Guid.Parse("f47c4144-e1d4-4817-1464-08d5fc6a9220"), DateTime.UtcNow, true, false, "Clínica"),
        //         new InstitutionType(Guid.Parse("0b96d09c-af50-4835-51e0-08d6286b7017"), DateTime.UtcNow, true, false, "Centro de Referência"),
        //         new InstitutionType(Guid.Parse("8ee17050-2220-4d7b-51e1-08d6286b7017"), DateTime.UtcNow, true, false, "Laboratório"),
        //     };
        //
        //     var institutionTypeId = _institutionTypes.First(x => x.Name == "Clínica").Id;
        //
        //     _institution = new Institution(
        //         Guid.Parse("8a8a4d7f-fbe4-4363-945a-656c4304e81c"),
        //         DateTime.UtcNow, 
        //         institutionTypeId,
        //         true,
        //         false,
        //         "Built Code Labs",
        //         new Identification("31909318000113", EIdentificationType.Cnpj),
        //         new AddressInformation
        //         {
        //             Address = "Alameda Francisco Alves",
        //             City = "Santo André",
        //             Complement = "8º andar, sala 81",
        //             Neighborhood = "Jardim",
        //             Number = "169",
        //             State = "SP",
        //             ZipCode = "09090790"
        //         },
        //         "Built Code Labs LTDA.",
        //         "Observation");
        //
        //     _industry = new Industry(
        //         Guid.Parse("239c0803-de32-4b56-b549-dfcf2283397e"),
        //         DateTime.UtcNow, 
        //         true,
        //         false,
        //         "Libs",
        //         "Indústria Farmaceutica");
        //
        //     _userAuthorizer = new User(
        //         Guid.Parse("8a8a4d7f-fbe4-4363-945a-656c4304e81c"),
        //         DateTime.UtcNow, 
        //         EUserType.System,
        //         true,
        //         false,
        //         "contato@builtcode.com.br",
        //         password,
        //         "Admin",
        //         "BuiltCode",
        //         "");
        //
        //     _userIndustry = new User(
        //         Guid.Parse("c072e084-6a39-4a43-bf3b-0270e0a719e2"),
        //         DateTime.UtcNow,
        //         EUserType.Industry,
        //         true,
        //         false,
        //         "user.industry@builtcode.com.br",
        //         password,
        //         "User",
        //         "Industry",
        //         "");
        //
        //     _userInstitution = new User(
        //         Guid.Parse("ca1108c7-5116-43ec-a6bd-f2acb32fc780"),
        //         DateTime.UtcNow,
        //         EUserType.Institution,
        //         true,
        //         false,
        //         "user.institution@builtcode.com.br",
        //         password,
        //         "User",
        //         "Institution",
        //         "");
        //
        //     _userDoctor = new User(
        //         Guid.Parse("f50fc0cb-1a35-43da-bbee-4563bd5c5a36"),
        //         DateTime.UtcNow,
        //         EUserType.Doctor,
        //         true,
        //         false,
        //         "user.doctor@builtcode.com.br",
        //         password,
        //         "User",
        //         "Doctor",
        //         "");
        // }
        //
        // public void Seed()
        // {
        //     AddInstitutionTypes()
        //         .AddInstitution()
        //         .AddIndustry()
        //         .AddUserAuthorizer()
        //         .AddUserIndustry()
        //         .AddUserInstitution()
        //         .AddUserDoctor();
        // }
        //
        // private SeedDevelopment AddInstitutionTypes()
        // {
        //     foreach (var item in _institutionTypes)
        //     {
        //         if (!_context.Set<InstitutionType>().Any(x => x.Id == item.Id))
        //             _context.Set<InstitutionType>().Add(item);
        //     }
        //
        //     _context.SaveChanges();
        //
        //     return this;
        // }
        //
        // private SeedDevelopment AddInstitution()
        // {
        //     if (_context.Set<Institution>().Find(_institution.Id) == null)
        //         _context.Add(_institution);
        //
        //     _context.SaveChanges();
        //
        //     return this;
        // }
        //
        // private SeedDevelopment AddIndustry()
        // {
        //     if (_context.Set<Industry>().Find(_industry.Id) == null)
        //         _context.Add(_industry);
        //
        //     _context.SaveChanges();
        //
        //     return this;
        // }
        //
        // private SeedDevelopment AddUserAuthorizer()
        // {
        //     if (!_context.Set<User>().Any(x => x.Email.Equals(_userAuthorizer.Email)))
        //     {
        //         _context.Add(_userAuthorizer);
        //         _context.Add(new UserSystem(_userAuthorizer.Id, EUserSystemProfile.Admin));
        //         _context.SaveChanges();
        //     }
        //
        //     return this;
        // }
        //
        // private SeedDevelopment AddUserIndustry()
        // {
        //     if (!_context.Set<User>().Any(x => x.Email.Equals(_userIndustry.Email)))
        //     {
        //         _context.Add(_userIndustry);
        //         _context.Add(new UserIndustry(_userIndustry.Id, _industry.Id));
        //         _context.SaveChanges();
        //     }
        //
        //     return this;
        // }
        //
        // private SeedDevelopment AddUserInstitution()
        // {
        //     if (!_context.Set<User>().Any(x => x.Email.Equals(_userInstitution.Email)))
        //     {
        //         _context.Add(_userInstitution);
        //         _context.Add(new UserInstitution(_userInstitution.Id,  _institution.Id));
        //         _context.SaveChanges();
        //     }
        //
        //     return this;
        // }
        //
        // private SeedDevelopment AddUserDoctor()
        // {
        //     if (!_context.Set<User>().Any(x => x.Email.Equals(_userDoctor.Email)))
        //     {
        //         _context.Add(_userDoctor);
        //         _context.Add(new UserDoctor(_userDoctor.Id, 
        //             new Phone
        //             {
        //                 Number = "123456",
        //                 Uf = "SP"
        //             }, new AddressInformation
        //             {
        //                 Address = "Alfredo Calux",
        //                 City = "São Bernardo do Campo",
        //                 Complement = "Jardim Calux",
        //                 Neighborhood = "Vila Planalto",
        //                 Number = "62",
        //                 State = "SP",
        //                 ZipCode = "09895595"
        //             }));
        //         _context.SaveChanges();
        //     }
        //
        //     return this;
        // }
    }
}