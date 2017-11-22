using Graph.Contracts;
using System.Collections.Generic;

namespace Graph.StronglyConnectedComponents
{
    public interface ISCC<T>
    {
        List<List<T>> FindSCG(Graph<T> graph);
    }
}
