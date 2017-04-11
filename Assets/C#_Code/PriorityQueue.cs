using System;
using System.Collections; 
using System.Collections.Generic;


public class PriorityQueue<T> 
{
    Dictionary<T, T> items;
    OrderingConvention convention; 
    public Int32 Count
    {
        get { return items.Count; }
    }
    public OrderingConvention Convention
    {
        get { return Convention; }
        set { Convention = value; }
    }
    public void Sort()
    {
       // if(OrderingConvention.Max)
    }
    public PriorityQueue(OrderingConvention myconvention)
    {
        this.convention = myconvention; 
    }
}
public enum OrderingConvention
{
    None, 
    Min,
    Max
}