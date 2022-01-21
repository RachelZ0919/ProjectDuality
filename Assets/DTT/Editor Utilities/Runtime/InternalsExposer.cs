using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DTT.EditorUtilities.Editor")]
[assembly: InternalsVisibleTo("DTT.EditorUtilities.Editor.Tests")]
namespace DTT.Utils.EditorUtilities
{
    // Used as a template without functionality to be able to expose internal members/classes to
    // other 'friend' assemblies using the 'InternalsVisibleToAttribute'.
}