using System;

namespace _17
{
    class Program
   {
    static string exp; //здесь хранится исходное выражение, которое ввел пользователь
    static string InBrackets; //здесь будут хранится выражения в скобках
    //true если символ под номером j в строке InBrackets является оператором + - * или /
    static bool NotOperator(int j)
    {
        return InBrackets[j] != '+' && InBrackets[j] != '-' && InBrackets[j] != '*' && InBrackets[j] != '/';
    }
 
    //возвращает левый операнд от оператора, который находится на i-ом месте в строке InBrackets
    static double GetLeftOperand(int i)
    {
        string LeftOperand = "";
        for (int j = i - 1; j >= 0; j--)
            if (NotOperator(j))
                LeftOperand = InBrackets[j] + LeftOperand;
            else
                break;
        return double.Parse(LeftOperand);
    }
 
    //возвращает правый операнд от оператора, который находится на i-ом месте в строке InBrackets
    static double GetRightOperand(int i)
    {
        string RightOperand = "";
        for (int j = i + 1; j < InBrackets.Length; j++)
            if (NotOperator(j))
                RightOperand += InBrackets[j];
            else
                break;
        return double.Parse(RightOperand);
    }
 
    //заменить выражение, оператор которого находится по номеру i в строке InBrackets на подсчитанное значение ToThis
    static void ReplaceExp(int i, double ToThis)
    {
        int FromI = 0;
        int ToI = InBrackets.Length - 1;
        for (int j = i - 1; j >= 0; j--)
            if (NotOperator(j))
                FromI = j;
            else
                break;
        for (int j = i + 1; j < InBrackets.Length; j++)
            if (NotOperator(j))
                ToI = j;
            else
                break;
        InBrackets = InBrackets.Replace(InBrackets.Substring(FromI, ToI - FromI + 1), ToThis.ToString());
    }
 
    //заменить умножение или деление в строке InBrackets на посчитанное значение
    static void repMD(int i)
    {
        double MD;
        if (InBrackets[i] == '*')
            MD = GetLeftOperand(i) * GetRightOperand(i);
        else
            MD = GetLeftOperand(i) / GetRightOperand(i);
        ReplaceExp(i, MD);
        Calc(); //вернуться в метод Calc - получается, что-то вроде взаимной рекурсии
    }
 
    //заменить сумму или разность в строке InBrackets на посчитанное значение
    static void repAS(int i)
    {
        double AS;
        if (InBrackets[i] == '+')
            AS = GetLeftOperand(i) + GetRightOperand(i);
        else
            AS = GetLeftOperand(i) - GetRightOperand(i);
        ReplaceExp(i, AS);
        Calc(); //вернуться в метод Calc - получается, что-то вроде взаимной рекурсии
    }
 
    //метод считающий чему равно выражение 
    static void Calc()
    {
        int i;
        //в первую очередь считаем умножение и деление
        for (i = 0; i < InBrackets.Length; i++)
            if (InBrackets[i] == '*' || InBrackets[i] == '/')
            {
                repMD(i); //отправляем номер символа в котором стоит оператор в метод repMD, он посчитает произведение или частное двух операндов стоящих вокруг него и заменит их на ответ
                return; //выход из взаимной рекурсии
            }
        //когда * и / закончились, начинаем считать + и -
        for (i = 0; i < InBrackets.Length; i++)
            if (InBrackets[i] == '+' || InBrackets[i] == '-')
            {
                repAS(i); //отправляем номер символа в котором стоит оператор в метод repAS, он посчитает сумму или разность двух операндов стоящих вокруг него и заменит их на ответ
                return; //выход из взаимной рекурсии
            }
    }
 
    
    static void Main()
    { 

        Console.Write("Введите выражение: ");
        exp =  Console.ReadLine().Replace(" ", "") ;
            InBrackets = exp;
        
            Calc(); //посчитать, чему равно выражение
           ///* exp = exp.Insert(0, InBrackets); //вставит*/ь посчитанное выражение в исходную строку
        
        Console.WriteLine("Ответ: " + InBrackets);
        Console.ReadKey();
    }
}
}
