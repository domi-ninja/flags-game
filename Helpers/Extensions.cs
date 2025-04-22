namespace flags_game;
public static class Extensions
{
  public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source, int seed=0)
  {
    Random rnd = new Random( seed );
    return source.OrderBy<T, int>((item) => rnd.Next());
  }
}