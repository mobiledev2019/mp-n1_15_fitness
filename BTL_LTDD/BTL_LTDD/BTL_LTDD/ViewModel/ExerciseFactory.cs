using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BTL_LTDD.ViewModel
{
    public static class ExerciseFactory
    {
        public static IList<Exercise> BeginExercise { get;private set; }
        public static IList<Exercise> AllExerciseAdult { get; private set; }
        public static IList<Exercise> AllExerciseChildren { get; private set; }
        static  ExerciseFactory()
        {
            AllExerciseChildren = new List<Exercise>
            {
                new Exercise
                {
                    NameExercise = $"boxing",
                    Type = "children",
                    ImageExercise = "boxing.PNG",
                    Time = "30 reps"
                },
                 new Exercise
                {
                    NameExercise = $"cycling",
                    Type = "children",
                    ImageExercise = "cycling.PNG",
                    Time = "20 reps"
                },
                  new Exercise
                {
                    NameExercise = $"dancing",
                    Type = "children",
                    ImageExercise = "dancing.PNG",
                    Time = "25 reps"
                },
                  new Exercise
                {
                    NameExercise = $"hiking",
                    Type = "children",
                    ImageExercise = "hiking.PNG",
                    Time = "15 reps"
                },
                  new Exercise
                {
                    NameExercise = $"jump rope",
                    Type = "children",
                    ImageExercise = "jum_rope.PNG",
                    Time = "15 reps"
                },
                  new Exercise
                {
                    NameExercise = $"rowing",
                    Type = "children",
                    ImageExercise = "rowing.PNG",
                    Time = "12 reps"
                },
                  new Exercise
                {
                    NameExercise = $"push up",
                    Type = "children",
                    ImageExercise = "jum_rope.PNG",
                    Time = "12 reps"
                },
                  new Exercise
                {
                    NameExercise = $"swimming",
                    Type = "children",
                    ImageExercise = "swimming.PNG",
                    Time = "25 reps"
                },
                  new Exercise
                {
                    NameExercise = $"walking",
                    Type = "children",
                    ImageExercise = "walking.PNG",
                    Time = "25 reps"
                }

            };

            AllExerciseAdult = new List<Exercise>
            {
                new Exercise
                {
                    NameExercise = $"boxing",
                    Type = "adult",
                    ImageExercise = "boxing.PNG",
                    Time = "30 reps"
                },
                 new Exercise
                {
                    NameExercise = $"cycling",
                    Type = "adult",
                    ImageExercise = "cycling.PNG",
                    Time = "20 reps"
                },
                  new Exercise
                {
                    NameExercise = $"dancing",
                    Type = "adult",
                    ImageExercise = "dancing.PNG",
                    Time = "25 reps"
                },
                  new Exercise
                {
                    NameExercise = $"hiking",
                    Type = "adult",
                    ImageExercise = "hiking.PNG",
                    Time = "15 reps"
                },
                  new Exercise
                {
                    NameExercise = $"jump rope",
                    Type = "adult",
                    ImageExercise = "jum_rope.PNG",
                    Time = "15 reps"
                },
                  new Exercise
                {
                    NameExercise = $"rowing",
                    Type = "adult",
                    ImageExercise = "rowing.PNG",
                    Time = "12 reps"
                },
                  new Exercise
                {
                    NameExercise = $"push up",
                    Type = "adult",
                    ImageExercise = "rowing.PNG",
                    Time = "12 reps"
                },
                  new Exercise
                {
                    NameExercise = $"swimming",
                    Type = "adult",
                    ImageExercise = "swimming.PNG",
                    Time = "25 reps"
                },
                  new Exercise
                {
                    NameExercise = $"walking",
                    Type = "adult",
                    ImageExercise = "walking.PNG",
                    Time = "25 reps"
                }

            };

            BeginExercise = new List<Exercise>
            {
                new Exercise
                {
                    //"hand start", "shoulder start", "waist start", "legs start"
                    NameExercise = "hand start",
                        ImageExercise = "walking.PNG",
                        Time = "10 reps"
                },
                 new Exercise
                {
                    NameExercise = "shoulder start",
                        ImageExercise = "hiking.PNG",
                        Time = "12 reps"
                },
                  new Exercise
                {
                    NameExercise =  "waist start",
                        ImageExercise = "jum_rope.PNG",
                        Time = "10 reps"
                },
                   new Exercise
                {
                    NameExercise = "legs start",
                        ImageExercise = "boxing.PNG",
                        Time = "12 reps"
                }
            };
        }
            
    }
}
