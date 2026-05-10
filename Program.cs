using OperationalBackendProgrammingRepetition.Catalogs;
using OperationalBackendProgrammingRepetition.Services;
using OperationalBackendProgrammingRepetition.UI;

var catalog = new Catalog();
var loanService = new LoanService();
var memberService = new MemberService();

ConsoleMenu menu = new ConsoleMenu(catalog, loanService, memberService);


menu.RunMenu();
