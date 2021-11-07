using System;
using System.IO;
using System.Linq;
using iText.IO.Source;
using iText.Kernel.Pdf;
using PDFSignature;

if (args.Length != 2)
{
    Console.WriteLine("Invalid args");
    args = new[] { "/Users/alexhome/Downloads/DoEveryStupidThing.pdf", "12" };
}
string inputFile = args[0];
int.TryParse(args[1], out int sigCount);

string workingPath = Path.GetDirectoryName(Path.GetFullPath(inputFile));
string outputName = Path.GetFileNameWithoutExtension(inputFile) + "-sig.pdf";
string outputFile = Path.Combine(workingPath, outputName);

MemoryStream sourceStream = new();
PdfDocument destDoc = new(new PdfWriter(outputFile));
PdfDocument sourceDoc = new(new PdfReader(inputFile), new PdfWriter(sourceStream));

int pageCount = sourceDoc.GetNumberOfPages();
// Add pages until pageCount % 4 == 0
while ((pageCount = sourceDoc.GetNumberOfPages()) % 4 != 0)
{
    sourceDoc.AddNewPage();
}
sourceDoc.Close();
if (sigCount * 4 > pageCount)
{
    throw new ArgumentException("Too many signatures.");
}
IRandomAccessSource source = new RandomAccessSourceFactory().CreateSource(sourceStream.ToArray());
sourceDoc = new(new PdfReader(source, new()));

// So, we have the total number of sigs, and the number of pages in each sig.
// Split pages by sig, then reorder the pages in each sig.
int[] pageNumbers = Enumerable.Range(1, pageCount).ToArray();
int[] orderedPageNumbers = Reorder.OrderBySigGroups(pageNumbers, sigCount).ToArray();

sourceDoc.CopyPagesTo(orderedPageNumbers, destDoc);

sourceDoc.Close();
sourceDoc.Close();
destDoc.Close();
