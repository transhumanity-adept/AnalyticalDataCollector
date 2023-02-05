using Elibrary;
using Loader;

// var loader = new Loader.Loader();
//
// loader.Print();


IElibraryParser parser = new ElibraryParser();
var getResponse= await parser.GetArticleByIdAsync(1);



//
// const int FROM_ID = 1;
// const int TO_ID = 100;

// Console.ForegroundColor = ConsoleColor.White;
// for (int i = FROM_ID; i <= TO_ID; i++)
// {
//     var getResponse = await parser.GetArticleByIdAsync(i);
//     if (getResponse.IsSuccess)
//     {
//         Console.BackgroundColor = ConsoleColor.Green;
//         Console.WriteLine($"SUCCESS {i}");
//         Console.ResetColor();
//     }
//     else
//     {
//         Console.BackgroundColor = ConsoleColor.Red;
//         Console.WriteLine($"BAD ID: {i}");
//         System.Console.WriteLine();
//         System.Console.WriteLine(getResponse.ErrorMessage);
//         System.Console.WriteLine();
//         Console.ResetColor();
//     }
// }