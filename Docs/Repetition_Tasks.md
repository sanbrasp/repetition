# Operational Programming
Repetition tasks

Made by: Claude AI

---

## Phase 1 — Foundation (tasks 1–5)

1. Model the core domain

Classes, encapsulation, properties

Create a Book class with private fields and public properties: Title, Author, ISBN, Year, and IsAvailable. Add a constructor that requires all fields. Override ToString() to return a readable summary. No setters on ISBN — it should be set only at construction.
encapsulationpropertiesconstructorsToString()

---

2. Add a Member class

Encapsulation, ID generation

Create a Member class with Name, Email, and a MemberId (auto-generated as a Guid or simple incrementing int — your choice). Add a method GetDisplayName() that returns a formatted string. The ID must be read-only after construction.
encapsulationread-onlyID generation

---

3. Build the Loan class

Object references, DateTime

Create a Loan class that holds a reference to a Book and a reference to a Member (not just IDs — actual object references). Include LoanDate and DueDate (e.g. 14 days after loan). Add a computed property IsOverdue that returns true if today is past the due date.
object referencesDateTimecomputed property

---

4. Introduce a Catalog helper class

Separation of concerns, List<T>

Create a BookCatalog class that wraps a private List<Book>. Add methods: AddBook(), RemoveBook(), FindByISBN(), FindByAuthor(), and GetAllBooks(). No business logic in Main — Main only calls catalog methods. This is your first separation of concerns practice.
separation of concernsList<T>helper classDRY

---

5. Build the console menu

Console UI, switch, loops

Build a working console menu loop in a separate ConsoleUI class (not in Program.cs). The menu should offer: Add book, List all books, Search by author, Quit. Use a do-while loop and a switch statement. No raw Console.ReadLine() outside of this class — wrap input reading in helper methods like ReadString() and ReadInt().
console menuseparation of concernshelper methodsloops

---

---

## Phase 2 — Polymorphism & abstraction (tasks 6–10)

1. Introduce an abstract base class

Abstraction, inheritance

Extract a LibraryItem abstract base class from Book. It should define shared properties (Title, ItemId, IsAvailable) and an abstract method GetItemType() that returns a string. Make Book inherit from it. This sets you up for a second item type soon.
abstract classinheritanceabstraction

---

2. Add a Magazine type

Inheritance, polymorphism

Create a Magazine class that also inherits from LibraryItem. It has IssueNumber and Publisher instead of Author. Implement GetItemType() differently than Book does. Update BookCatalog (or rename it to Catalog) to work with List<LibraryItem> so it can hold both.
polymorphisminheritanceList<T> with base type

---

3. Add an interface for searchable items

Interfaces, polymorphism

Define an ISearchable interface with a method MatchesQuery(string query) returning bool. Implement it on both Book and Magazine with their own logic (Book searches title+author, Magazine searches title+publisher). Add a Search() method to Catalog that works against ISearchable — no type-checking with 'is' or casting allowed.
interfacepolymorphismno type-casting

---

4. Create a LoanService

Business logic separation, object references

Create a LoanService class that manages all lending logic. It holds a private List<Loan>. Add methods: LoanItem(LibraryItem, Member), ReturnItem(LibraryItem), GetLoansForMember(Member), and GetOverdueLoans(). LoanItem should set IsAvailable = false on the item and throw an exception if it's already on loan.
business logicseparation of concernsexception handlingobject references

---

5. Add a MemberService

DRY, service layer pattern

Create a MemberService that manages List<Member>. Add: RegisterMember(), FindById(), FindByName(), and GetAllMembers(). Notice how similar this feels to your Catalog class — that's the service pattern. The two services should be independent; no service calls the other directly.
service layerDRYList<T>separation of concerns

---

---

## Phase 3 — Collections, references & data integrity (tasks 11–15)

1. Wire everything into the menu

Dependency, composition, clean Main

Refactor Program.cs so it creates exactly one instance each of Catalog, LoanService, MemberService, and ConsoleUI — then passes them where needed. ConsoleUI should receive its dependencies via constructor. Main should be 5–10 lines max. Add menu options: Register member, Loan item, Return item, View member loans.
compositiondependency injection (basic)clean Mainconstructor injection

---

2. Use LINQ to query collections

LINQ, collections

Replace any manual loops that filter or sort with LINQ equivalents. Add: GetAvailableBooks(), GetOverdueLoans() using .Where(), and in ConsoleUI add a 'List overdue loans' option that shows member name + item title + days overdue, sorted by most overdue first using .OrderByDescending().
LINQWhere()OrderByDescending()Select()

---

3. Validate input and handle exceptions gracefully

Exception handling, validation

Add a Validate() method or static ValidationHelper class with methods like RequireNotEmpty(string, string fieldName) and RequirePositive(int). Throw ArgumentException with clear messages. In ConsoleUI, wrap service calls in try-catch and print friendly error messages — never let an unhandled exception crash the app.
exception handlingvalidationhelper classDRY

---

4. Add loan history per member

References, collections, DRY

Extend LoanService so GetLoanHistoryForMember(Member m) returns all loans (including returned ones) for that member. Mark returned loans with a ReturnedDate (nullable DateTime). Update the menu to show a member's full loan history, including whether each item was returned on time.
nullable typesDateTimecollectionsreferences

---

5. Add a reporting helper

Helper methods, DRY, LINQ

Create a ReportService with methods: GetMostActiveMember() (member with most loans), GetMostBorrowedItem(), and GetAverageLoanDurationDays(). Use LINQ throughout. Wire a 'Reports' submenu into ConsoleUI. No repeated code — if two reports share a calculation, extract a shared private method.
LINQhelper methodsDRYGroupBy()submenus

---

---

## Phase 4 — JSON, persistence & full app (tasks 16–20)

1. Serialize the catalog to JSON

JSON serialization, System.Text.Json

Add a PersistenceService with SaveCatalog(List<LibraryItem> items, string path) and LoadCatalog(string path) methods using System.Text.Json. Since LibraryItem is abstract, you'll need to handle polymorphic serialization (hint: JsonDerivedType attribute or a custom converter). Save to a file called catalog.json.
JSONSystem.Text.Jsonserializationpolymorphism

---

2. Serialize members and loans

JSON, ID-based references

Serialize Members and Loans separately. Loans must store MemberId and ItemId (strings/ints) rather than the full objects — you can't serialize circular references. Add SaveAll() and LoadAll() to PersistenceService that saves all three files.
JSONID referencesdeserializationcircular reference avoidance

---

3. Reconnect object references after loading

Deserialization, ID-to-object reconnection

After loading from JSON, Loan objects have MemberId and ItemId but no live references. Write a ReconnectReferences(List<Loan>, List<Member>, List<LibraryItem>) method in PersistenceService that looks up and assigns the actual Member and LibraryItem objects. This is a key real-world pattern.
deserializationID reconnectionobject graphdictionary lookup

---

4. Auto-save and auto-load on startup/exit

App lifecycle, persistence

Call LoadAll() at startup and SaveAll() before exit. Display a loading message while reading files, and handle the case where no save files exist yet (first run). Use a try-catch around all file I/O. If loading fails, offer to start fresh rather than crashing.
app lifecyclefile I/Oexception handlingUX

---

5. Final polish — clean code review

DRY, naming, separation of concerns

Do a full code review pass. Find and remove any duplicated logic. Check that every class has one clear responsibility. Rename anything confusingly named. Ensure no business logic lives in ConsoleUI and no console output lives in services. As a final feature, add a 'Seed demo data' option that populates books, members, and loans for easy testing.
DRYclean codeseparation of concernsrefactoringseeding data

---

---

The 20 tasks are split into four phases:
- Phase 1 (1–5) gets the core domain modeled — Book, Member, Loan, a catalog, and a working console menu. By task 5 you have a running app.


- Phase 2 (6–10) introduces the OOP concepts — abstract classes, inheritance, interfaces, polymorphism, and a proper service layer. The app starts growing real behavior.


- Phase 3 (11–15) adds LINQ, exception handling, input validation, loan history, and reporting. This is where clean code principles really get tested.


- Phase 4 (16–20) is all about persistence — JSON serialization of an abstract type hierarchy, saving ID references instead of objects, 
and then the really interesting one: reconnecting those IDs back to live objects after loading. Task 20 is a refactoring/polish pass so you end with code you're proud of.



