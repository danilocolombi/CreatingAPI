namespace CreatingAPI.Domain.Unscrambles.ValueObjects
{
    public class ShuffledExercise
    {
        public string Part1 { get; private set; }
        public string Part2 { get; private set; }
        public string Part3 { get; private set; }
        public string Part4 { get; private set; }
        public string CompleteExercise { get; private set; }

        public ShuffledExercise(string part1, string part2, string part3, string part4, string completeExercise)
        {
            Part1 = part1;
            Part2 = part2;
            Part3 = part3;
            Part4 = part4;
            CompleteExercise = completeExercise;
        }
    }
}
