using System.Collections;

public static class Tuple {

    public static Tuple<T1> Create<T1>(T1 first){
        return new Tuple<T1>(first);
    }

    public static Tuple<T1, T2> Create<T1, T2>(T1 first, T2 second){
        return new Tuple<T1, T2>(first, second);
    }

    public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 first, T2 second, T3 third){
        return new Tuple<T1, T2, T3>(first, second, third);
    }

    public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 first, T2 second, T3 third, T4 fourth){
        return new Tuple<T1, T2, T3, T4>(first, second, third, fourth);
    }

    public static Tuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 first, T2 second, T3 third, T4 fourth, T5 fifth){
        return new Tuple<T1, T2, T3, T4, T5>(first, second, third, fourth, fifth);
    }

    public static Tuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth){
        return new Tuple<T1, T2, T3, T4, T5, T6>(first, second, third, fourth, fifth, sixth);
    }

    public static Tuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh){
        return new Tuple<T1, T2, T3, T4, T5, T6, T7>(first, second, third, fourth, fifth, sixth, seventh);
    }

    public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth){
        return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8>(first, second, third, fourth, fifth, sixth, seventh, eighth);
    }
}

public class Tuple<T1>
{
    public T1 First { get; private set; }
    internal Tuple(T1 first)
    {
        First = first;
    }
}

public class Tuple<T1, T2>
{
    public T1 First { get; private set; }
    public T2 Second { get; private set; }
    internal Tuple(T1 first, T2 second)
    {
        First = first;
        Second = second;
    }
}

public class Tuple<T1, T2, T3>
{
    public T1 First { get; private set; }
    public T2 Second { get; private set; }
    public T3 Third { get; private set; }
    internal Tuple(T1 first, T2 second, T3 third)
    {
        First = first;
        Second = second;
        Third = third;
    }
}

public class Tuple<T1, T2, T3, T4>
{
    public T1 First { get; private set; }
    public T2 Second { get; private set; }
    public T3 Third { get; private set; }
    public T4 Fourth { get; private set; }
    internal Tuple(T1 first, T2 second, T3 third, T4 fourth)
    {
        First = first;
        Second = second;
        Third = third;
        Fourth = fourth;
    }
}

public class Tuple<T1, T2, T3, T4, T5>
{
    public T1 First { get; private set; }
    public T2 Second { get; private set; }
    public T3 Third { get; private set; }
    public T4 Fourth { get; private set; }
    public T5 Fifth { get; private set; }
    internal Tuple(T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
    {
        First = first;
        Second = second;
        Third = third;
        Fourth = fourth;
        Fifth = fifth;
    }
}

public class Tuple<T1, T2, T3, T4, T5, T6>
{
    public T1 First { get; private set; }
    public T2 Second { get; private set; }
    public T3 Third { get; private set; }
    public T4 Fourth { get; private set; }
    public T5 Fifth { get; private set; }
    public T6 Sixth { get; private set; }
    internal Tuple(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
    {
        First = first;
        Second = second;
        Third = third;
        Fourth = fourth;
        Fifth = fifth;
        Sixth = sixth;
    }
}

public class Tuple<T1, T2, T3, T4, T5, T6, T7>
{
    public T1 First { get; private set; }
    public T2 Second { get; private set; }
    public T3 Third { get; private set; }
    public T4 Fourth { get; private set; }
    public T5 Fifth { get; private set; }
    public T6 Sixth { get; private set; }
    public T7 Seventh { get; private set; }
    internal Tuple(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
    {
        First = first;
        Second = second;
        Third = third;
        Fourth = fourth;
        Fifth = fifth;
        Sixth = sixth;
        Seventh = seventh;
    }
}

public class Tuple<T1, T2, T3, T4, T5, T6, T7, T8>
{
    public T1 First { get; private set; }
    public T2 Second { get; private set; }
    public T3 Third { get; private set; }
    public T4 Fourth { get; private set; }
    public T5 Fifth { get; private set; }
    public T6 Sixth { get; private set; }
    public T7 Seventh { get; private set; }
    public T8 Eighth { get; private set; }
    internal Tuple(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
    {
        First = first;
        Second = second;
        Third = third;
        Fourth = fourth;
        Fifth = fifth;
        Sixth = sixth;
        Seventh = seventh;
        Eighth = eighth;
    }
}