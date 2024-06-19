namespace flags_game;
public static class Extensions
{
  public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
  {
    Random rnd = new Random( );
    return source.OrderBy<T, int>((item) => rnd.Next());
  }
}