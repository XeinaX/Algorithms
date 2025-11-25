using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public class Solution
    
{
    static void Main()
    {
       
    }
    //258 
    //задача уровня изи так что нечего говорить по сути просто сложить все со всем самое сложное просто правильно спарсить char в инт (нужно переводить в строку отдельный char иначе будет считать char по таблице)
    public static int Anything(int num)
    {
        string srtring = num.ToString();
        if (srtring.Length == 1)
        {
            return num;
        }
        int sumanything = 0;
        while (srtring.Length > 1)
        {
            sumanything = 0;
            foreach (char number in srtring)
            {
                int bukva = Int32.Parse(number.ToString());
                sumanything += bukva;
            }
            srtring = sumanything.ToString();
        }
        return sumanything;
    }

    public static int Fuction1(int input)
    {
        var ab = input.ToString().Select(n => n - '0').Order().ToList();
        if(ab.FirstOrDefault() == 0) 
        {
            foreach (int number in ab)
            {
                if (number != 0)
                {
                    int FirstSaveNumber = ab.First();
                    ab[0] = FirstSaveNumber;
                    ab[ab.IndexOf(number)] = 0;
                    break;
                }
            }
        }
        var builder = new StringBuilder();
        for (int i = 0; i < ab.Count; i++)
        {
            builder.Append(ab[i]);
        }

        return int.Parse(builder.ToString());   
    }
   
    public bool CanConstruct(string ransomNote, string magazine)
    {
        Dictionary<char, int> dic = new Dictionary<char, int>();

        foreach (char bukva in magazine)
        {
            if (dic.ContainsKey(bukva))
            {
                dic[bukva]++;
            }
            else
            {
                dic.Add(bukva, 1);
            }
        }
        foreach (char bukva in ransomNote)
        {
            if (dic.ContainsKey(bukva))
            {
                dic[bukva]--;
                if (dic[bukva] < 0)
                {
                    return false;
                }
            }
            else{
                return false;
            }
        }
        return true;
    }

    //503.1
    //правильное решение немного другой задачи (неправильно понял условия но сделал как надо)
    //задача 503 но с другим условием: заменить каждый элемент массива на ближайший к нему больший, если такого нет то -1
    //пример: [5,4,3,2,1] -> [-1,5,4,3,2]; [1,2,3,4,3] -> [2,3,4,-1,4]
    public int[] AnotherNextGreaterElements(int[] nums)
    {
        int[] numscopy = new int[nums.Length];
        Array.Copy(nums, numscopy, numscopy.Length);
        for (int i = 0; i < nums.Length; i++)
        {
            int max = numscopy.Max();
            if (nums[i] == numscopy.Max())
            {
                nums[i] = -1;
            }
            foreach (int num in numscopy)
            {
                if (num <= max & num > numscopy[i] & nums[i] != -1)
                {
                    max = num;
                    nums[i] = max;
                }
            }
        }
        return nums;
    }

    //503.2
    //опять правильное решение другой задачи (неправильно понял условия но сделал как надо)
    //задача 503 но с другим условием: заменить каждый элемент массива на ближайший к нему больший, но при условии что можно смотреть не дальше след элемента
    //(то есть по сути постоянное нахождение все большего элемента и замена текущего на него) , если такого нет то -1
    //пример: [5,4,3,2,1] -> [-1,5,5,5,5]; [1,2,3,4,3] -> [2,3,4,-1,4] 
    public int[] AnotherNextGreaterElements2(int[] nums)
    {
        List<int> list = nums.ToList();
        list.Add(0);
        int max = nums[0];
        for (int i = 0; i < nums.Length; i++)
        {
            if (list[i + 1] > max)
            {
                max = nums[i + 1];
                nums[i] = max;
            }
            else if (nums[i] < max)
            {
                nums[i] = max;
            }
            else if (nums[i] == nums.Max())
            {
                nums[i] = -1;
            }
        }
        return nums;
    }

    //задача от товарища
    //На вход, как аргумент в функцию, дается строка s из следующих скобочек: '(', ')', '{', '}', '[' и ']', выведи правильно ли они расположены.
    //    Правильно это если:
    //1)Открытые скобки должны закрываться скобками того же типа.
    //2)Открытые скобки должны закрываться в правильном порядке.
    //3)Каждой закрывающей скобке соответствует открытая скобка того же типа.
    //пример: () -> true; (] -> false; )( -> false; ()[]{} -> true 
    public static bool Brackets(string brackets)
    {
        Stack<char> stackchars = new Stack<char>();
        foreach (char c in brackets)
        {
            if (c != ')' & c != ']' & c != '}')
            {
                stackchars.Push(c);
            }
            else if (stackchars.Count > 0)
            {
                if (c == ')')
                {
                    if (stackchars.Peek() == '(')
                    {
                        stackchars.Pop();
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (c == ']')
                {
                    if (stackchars.Peek() == '[')
                    {
                        stackchars.Pop();
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (c == '}')
                {
                    if (stackchars.Peek() == '{')
                    {
                        stackchars.Pop();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

        }
        if (stackchars.Count > 0)
        {
            return false;
        }
        else { return true; }

    }
    //Прикольная реализация от GPT (сам как обычно тупанул и пошел прямым путем)
    public static bool GPTBrackets(string brackets)
    {
        Stack<char> stackchars = new Stack<char>();

        // Словарь соответствия открывающих и закрывающих скобок
        Dictionary<char, char> bracketPairs = new Dictionary<char, char>
    {
        { ')', '(' },
        { ']', '[' },
        { '}', '{' }
    };

        foreach (char c in brackets)
        {
            // Если символ — закрывающая скобка
            if (bracketPairs.ContainsKey(c))
            {
                // Проверяем, есть ли в стеке открывающая скобка и соответствует ли она текущей
                if (stackchars.Count == 0 || stackchars.Pop() != bracketPairs[c])
                {
                    return false;
                }
            }
            else
            {
                // Если символ — открывающая скобка, кладём его в стек
                stackchars.Push(c);
            }
        }

        // Если стек пуст, скобки правильно сбалансированы
        return stackchars.Count == 0;
    }

    //14
    //ужасная реализация данной задачи(хотелось попробовать что-то с рекурсией и в итоге ничего хорошего не вышло)
    //пример: ["flower","flow","flight"] -> "fl"; ["dog","racecar","car"] -> ""
    public static string LongestCommonPrefix(string[] strs)
    {
        List<char> list = new List<char>();

        if (strs.Length > 1 & strs[0] != "") 
        {
            for (int i = 0; i < strs[0].Length; i++)
            {
                list.Add(strs[0][i]);
                foreach (string c in strs)
                {
                    if (Rec(c, 0, i) == false & list.Count >= 1)
                    {
                        list.RemoveAt(list.Count - 1);
                        return string.Concat(list);
                    }
                }
                if (Rec(strs[0], 0, i) == true & i == strs[0].Length - 1)
                {
                    return string.Concat(list);
                }
            }
        }
        else
        {
            return strs.First();
        }

        bool Rec(string str, int i, int j)
        {
            try
            {
                if (i > j)
                {
                    return true;
                }
                if (str[i] == list[i])
                {
                    i++;
                    if (Rec(str, i, j))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch { return false; }
        }
        return null;
    }
    public bool BackspaceCompare(string s, string t)
    {

        return ChangeString(s) == ChangeString(t);
    }

    public static string ChangeString(string str)
    {
       StringBuilder stringBuilder = new StringBuilder(str);
        int i = 0;
        while (i <= stringBuilder.Length-1)
        {
           if (stringBuilder[i] == '#' & i == 0)
            {
                stringBuilder.Remove(i, 1);
                i--;
            }
            else if (stringBuilder[i] == '#')
            {
               stringBuilder.Remove(i-1, 2);
                if (i >= 1)
                {
                    i-=2;
                }
            }
            i++;
        }
        return stringBuilder.ToString();
    }

    public bool IsBalanced(string num)
    {
        int even = 0;
        int odd = 0;
        for (int i = 0; i < num.Length; i++)
        {
            if (i % 2 == 0) { even += int.Parse(num[i].ToString()); }
            else { odd += Convert.ToInt32(num[i].ToString());}
        }
        return even == odd;
    }

    //3169 
    //опять ужасная реализация данной задачи(хотелось попробовать сделать микроциклы и сделать максимально топорную реализацию чтобы порабоать с if и простраивать логику в голове)
    //пример: days = 10, meetings = [[5,7],[1,3],[9,10]] -> 2; days = 5, meetings = [[2,4],[1,3]] -> 1
    public int CountDays(int days, int[][] meetings)
    {
        int count = 0;
        Array.Sort(meetings,(a,b) => a[0].CompareTo(b[0]));
        HashSet<int> uniqueNumbers = new HashSet<int>();
        int minday = meetings[0][0];
        int maxday = meetings[0][1];

        foreach (var meeting in meetings)
        {
            if (minday >= meeting[0]) 
            {
                if (minday > meeting[1])
                {
                    for (int i = 1; i < minday - meeting[1]; i++)
                    {
                        uniqueNumbers.Add(meeting[1] + i);
                    }
                }
                else {
                    if(maxday < meeting[1])
                    {
                        maxday = meeting[1];
                    }
                }
                    minday = meeting[0];

            } else if(minday < meeting[0]) {
                if(maxday < meeting[0])
                {
                    for (int i = 1; i < meeting[0] - maxday; i++)
                    {
                        uniqueNumbers.Add(maxday + i);
                    }
                    maxday = meeting[1];
                }
                else
                {
                    if(maxday < meeting[1]) {
                        maxday = meeting[1];
                    }
                }
            }
        }
        if (maxday < days) {
            count += days - maxday;
        
        }if(minday > 1) {
            count += minday-1;
        }
        return uniqueNumbers.Count+count;
    }

    //42

    //неудачный вариант 
    //public int Trap(int[] height)
    //{
    //    int count = 0;
    //    int i = 0;
    //    while (i < height.Length)
    //    {
    //        int[] nums = FindFirstGreaterValue(height);
    //        if (height[i] <= nums[i]) {
    //            count += nums[i]- height[i]-1;
    //        }
    //        height[i]--;
    //        if (height[i] > 0) {
    //            i--;
    //        }
    //        i++;
    //    }
    //    return 0;
    //}
    //Вспомогательная функция для 42 которая ищет для каждого значения в массиве первое большее если такого нет просто оставляет это число
    public static int[] FindFirstGreaterValue(int[] arr)
    {
        int[] result = new int[arr.Length];
        Stack<int> stack = new Stack<int>();

        for (int i = arr.Length - 1; i >= 0; i--)
        {
            while (stack.Count > 0 && arr[stack.Peek()] < arr[i])
            {
                stack.Pop();
            }
            result[i] = stack.Count > 0 ? arr[stack.Peek()] : arr[i];

            stack.Push(i);
        }

        return result;
    }
    //Не совсем удачная попытка решить 42(точнее удачная, но не эффективная из-за прохода по горизонтали полностью
    public int Trap1(int[] height)
    {
        int max = height.Max();
        int count = 0;
       
        for (int i = 0; i < max; i++)
        {
            int index =  -1;
            
            for (int j = 0; j < height.Length; j++)
            {
                if (height[j] >= max - i)
                {
                    if (index == -1)
                    {
                        index = j;
                    }
                    else
                    {
                        count += j - index-1;
                        index = j;
                    }
                }
            }
        }
        return count;
    }

    public int Trap2(int[] height)
    {
        return 0;
    }
    public static int Jump(int[] nums)
    {
        int jumps = 0, farthest = 0, end = 0;

        for (int i = 0; i < nums.Length - 1; i++)
        {
            // Update the farthest point we can reach
            farthest = Math.Max(farthest, i + nums[i]);

            // If we've reached the current end, we need to jump
            if (i == end)
            {
                jumps++;
                end = farthest; // Update the range for the next jump
            }
        }

        return jumps;
    }
    public bool IsToeplitzMatrix(int[][] matrix)
    {
        for (int i = 0; i < matrix.Length-1; i++)
        {
            for (int j = 0; j < matrix[0].Length-1; j++)
            {
                if (matrix[i+1][j+1] != matrix[i][j])
                {
                    return false;
                }
            }
        }
        return true;
    }

//2200 странная задача, не понравилась вообще. Логика у задачи неприятная и думать не удобно было хотя  по своей сути очень даже простая, но как обычно тупил долго. Самое непонятное было когда менять текущий key на след чтобы сравнивать уже с ним, но как обычно используя не очень хороший if все решается.
    public IList<int> FindKDistantIndices(int[] nums, int key, int k)
    {
        var keys = new List<int>();
        var answer = new List<int>();

        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == key) keys.Add(i);
        }

        int j = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            if (Math.Abs(i - keys[j]) <= k)
            {

                answer.Add(i);
            }
            if (keys[j] + k <= i && j != keys.Count-1)
            {
                j++;
            }
        }

        return answer;
    }

    //1812 супер фигня, а не задача надо просто вернуть: белая клетка на шахматной доске или черная по координатам, которые дают в задаче. Основная проблема просто как-то по интересному обработать буквы. Идея в том чтобы складывать просто коды char символов и все, а дальше просто чет или нечет.
    public static bool SquareIsWhite(string coordinates)
    {
        char[] chars = coordinates.ToCharArray();

        if ((chars[0] + chars[1]) % 2 == 1)
        {
            return true;
        } else
        {
            return false;
        }
    }
}

//225 обычная задача, но при этом все равно решил не с первого раза(можно было сделать по другому, но ладно)
public class MyStack
{
    public Queue<int> stack1 { get; set; }
    public MyStack()
    {
        stack1 = new Queue<int>();
    }

    public void Push(int x)
    {
       stack1.Enqueue(x);
    }

    public int Pop()
    {
        int[] stacks = new int[stack1.Count];
        stack1.CopyTo(stacks,0);
        for (int i = 0; i < stack1.Count-1; i++)
        {
            stack1.Dequeue();
            stack1.Enqueue(stacks[i]);
        }
        return stack1.Dequeue();
       
    }

    public int Top()
    {
        int top = 0;
        int[] stacks = new int[stack1.Count];
        stack1.CopyTo(stacks, 0);
        for (int i = 0; i < stack1.Count; i++)
        {
            if (i == stack1.Count-1)
            {
                top = stack1.Peek(); 
            }
            stack1.Dequeue();
            stack1.Enqueue(stacks[i]);
        }
        return top;
    }

    public bool Empty()
    {
        if(stack1.Count == 0)
        {
            return true;
        }
        else 
        { 
            return false;
        }
    }
   

}


//1

//    string first = "ab";
//string second = "a" + "b";

//Console.WriteLine(first == second);
//Console.WriteLine((object)first == (object)second);

//2
//    Dictionary<Key, string> dictionary = new Dictionary<Key, string>();
//Key firstKey = new Key(1);
//dictionary.Add(firstKey, "First");
//Key secondKey = new Key(2);
//dictionary.Add(secondKey, "Second");

//Console.WriteLine(dictionary[firstKey]);

//public class Key
//{
//    public int Marker { get; }

//    public Key(int marker) => Marker = marker;

//    public override int GetHashCode() => Marker / 10;

//    public override bool Equals(object? other) =>
//        other is Key ? other.GetHashCode() == GetHashCode() : base.Equals(other);
//}






//3




//    string key = "a";

//try
//{
//    throw new ArgumentException();
//}
//catch (ArgumentException)
//{
//    key += "b";
//}
//catch (Exception)
//{
//    key += "c";
//}
//finally
//{
//    key += "d";
//}

//4



//    try
//{
//using (Robot robot = new Robot())
//{
//robot.Dispose();
//throw new InvalidOperationException();
//}
//}
//catch (InvalidOperationException)
//{
//Console.WriteLine("Exception handled");
//}

//public class Robot : IDisposable
//{
//    public Robot() => Console.WriteLine("Constructed");
//    public void Dispose() => Console.WriteLine("Disposed");
//}


//6

//    Ultrabot ultrabot = new Ultrabot();
//Robot robot = new Robot();

//public class Robot
//{
//    static Robot() { Console.WriteLine("Static"); }
//    public Robot() { Console.WriteLine("Robot"); }
//}

//public class Ultrabot : Robot
//{
//    public Ultrabot() : base() { Console.WriteLine("Ultrabot"); }
//}

//5

//int first = 0;
//object second = (object)first;
//Increment(ref first);
//Console.WriteLine(first == (int)second);

//public static void Increment(ref int source)
//{
//    source++;
//}


//7


//Fuzzbot bot = new Fuzzbot();
//Console.WriteLine(bot is Robot);
//Console.WriteLine(bot is Fuzzbot);
//Console.WriteLine(bot is Buzzbot);

//public class Robot { }

//public class Fuzzbot : Robot { }

//public class Buzzbot : Robot { }

//8

//Human human = new Human();
//Robot robot = new Robot(human);
//robot.HumanOperator.Name = "Masha";
//Console.WriteLine(robot.HumanOperator.Name);

//public class Robot
//{
//    public readonly Human HumanOperator;
//    public Robot(Human humanOperator) => HumanOperator = humanOperator;
//}

//public class Human
//{
//    public string Name;
//}

//9


//Robot robot = new Robot();
//robot.Print();
//(robot as Robot).Print();
//(robot as BaseRobot).Print();

//public abstract class BaseRobot
//{
//    public virtual void Print() => Console.WriteLine("BaseRobot");
//}

//public class Robot : BaseRobot
//{
//    public override void Print() => Console.WriteLine("Robot");
//}

//10

//Robot robot = new Robot();
//robot.Print();
//(robot as Robot).Print();
//(robot as BaseRobot).Print();

//public abstract class BaseRobot
//{
//    public void Print() => Console.WriteLine("BaseRobot");
//}

//public class Robot : BaseRobot
//{
//    public new void Print() => Console.WriteLine("Robot");
//}

//11

//Queue<string> queue = new Queue<string>();
//queue.Enqueue("1");
//queue.Enqueue("2");
//queue.Dequeue();
//queue.Enqueue("3");

//foreach (string item in (IEnumerable<string>)queue)
//{
//    Console.WriteLine(item);
//}


//12

//Container container = new Container() { Value = 1 };
//Container.Nullify(container);
//Console.WriteLine(container.Value);

//public struct Container
//{
//    public int Value;
//    public static void Nullify(Container container) => container.Value = 0;
//}

//13

//List<int> numbers = new List<int> { 1, 2, 3, 4 };
//IEnumerable<int> squares = numbers
//    .Where(x => x % 2 == 0)
//    .Select(x => x * 2);

//foreach (var square in squares)
//{
//    Console.WriteLine(square);
//}

