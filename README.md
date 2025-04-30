
```markdown
# 📚 SmartBook - Bibliotekssystem i konsolen

---

## 🧰 Funktioner

I den här applikationen kan du:

-  **Lägga till en bok** (titel, författare, ISBN, kategori)
-  **Ta bort en bok** (via titel eller ISBN)
-  **Lista alla böcker** (sorterade med LINQ)
-  **Söka efter böcker** (efter titel eller författare – med LINQ)
-  **Markera bok som 'utlånad' 📕 eller 'tillgänglig' 📗**
-  **Spara biblioteket** till fil (.json)
-  **Ladda biblioteket** från fil
-  **Skriva och köra grundläggande tester** med xUnit

---

## 🏗️ Projektstruktur

- `Program.cs` – Startpunkt och menyhantering
- `LibraryApp.cs` – Logik och interaktion med användaren
- `Library.cs` – Hantering av boklistan (`List<Book>`)
- `Book.cs` – Klass som beskriver en bok
- `LibraryTests.cs` – Tester med xUnit (sökning, validering etc.)

---

### ✅ Enhetstester

Enhetstester har skrivits med **xUnit** för att säkerställa att bibliotekssystemets funktioner fungerar som förväntat.

#### Testfil: `LibraryTests/UnitTest1.cs`

| Testnamn                                     
|--------------------------------------------     
| `AddBook_ShouldAddBook_WhenBookIsValid`          
| `AddBook_ShouldNotAddBook_WhenDuplicateISBN`       
| `RemoveBookByTitle_ShouldRemoveBook_WhenBookExists` 
| `SearchBooks_ShouldReturnMatchingBooks_WhenSearchTermMatches` 
| `SaveToJson_ShoudlSaveBookToJsonFile`                    
| `LoadFromJson_ShouldLoadBooksFromJsonFile`   

---


## 🔧 Tekniker som används

- **C# / .NET**
- **LINQ** för filtrering och sortering
-  **System.Text.Json** för att läsa/spara data
-  **xUnit** för tester
- 🛡 **Validering** för att undvika dubbletter (baserat på ISBN)
- **Felsäkerhet** via `try/catch` och användarvänliga felmeddelanden

---

## 🚀 Kom igång

1. Klona projektet:
   ```bash
   git clone https://github.com/E-Rx/lexicon_smartbook.git
   ```
2. Öppna i Visual Studio eller VS Code
3. Kör programmet med:
   ```bash
   dotnet run
   ```
4. Använd menyvalen för att utforska 📖

---

## 🧪 Köra tester

Säkerställ att testprojektet är rätt inställt och kör:
```bash
dotnet test
```

---

## 🎯 Bonus / Fördjupning (valfritt)

- 👤 Lägg till enklare logik för **användare/lånekort**
- 📝 Exportera en **rapport (.txt)** med alla böcker som är **utlånade**
- 🕒 Implementera **enkel historik/logg** över åtgärder som sparas i minnet eller på disk

