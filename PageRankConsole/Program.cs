using System;
using System.Collections.Generic;

class Program
{
  class Graph
  {
      public int maxIndex = 64;
      public int[,] graphMatrix = new int[100, 100];
      public float[] PageRanks = new float[100];
    

      public Graph(int num)
      {
          //graphMatrix[4, 4] = 1;

          maxIndex = num;
      }
      public void Show()
      {
          Console.Clear();
          for (int i = 0; i < maxIndex; i++)
          {
              string temp = "";
              for (int k = 0; k < maxIndex; k++)
              {
                  temp += graphMatrix[i, k].ToString() + ", ";
              }
              Console.WriteLine(Convert.ToChar(i+65) + ": " + temp);
          }
      }
  }
  private class PageLink : Graph
  {
      public PageLink(int num) : base(num)
      {
          for (int x = 0; x < 100; x++)
          {
              PageRanks[x] = 1f;
          }
      }

      public int GetvalidLetter()
      {
          int charindex = 0;
          bool valid = false;
          string input = "";
          while (valid == false)
          {
              input = Console.ReadLine();
              char.Parse(input);
              input = input.ToUpper();
              Console.WriteLine(input);
              charindex = input[0];

              if (input.Length > 1)
              {
                  Console.WriteLine("One letter only");
              }

              else if (charindex > (65 + maxIndex) || charindex < 65)
              {
                  if (input == "X")
                  {
                      valid = true;
                  }
                  Console.WriteLine("Enter letter A to number of pages");
              }
              else
              {
                  valid = true;
              }
          }
          return charindex;
      }

      public void PagesSetUp()
      {
          for (int i = 0; i < maxIndex; i++)
          {
              Console.Clear();
              Console.WriteLine("Page: " + Convert.ToChar(i+65));

              bool cont = true;
              while (cont)
              {
                  Console.WriteLine("Link this page to: (or 'X' to skip)");
                  int index = GetvalidLetter();
                  if (index == 88)
                  {
                      cont = false;
                  }
                  else
                  {
                      graphMatrix[i, index - 65] = 1;
                      Console.Clear();
                  }
              }

          }
          
      }

      public void PagePreSet(int[,] example)
      {
          graphMatrix = example;
      }

      public void Summary(int itterations, float d)
      {
          CalcPR(itterations, d);
          Console.WriteLine("\n\nAfter " + itterations + " iterations: (while d = " + d + ")\n----------------------------------------------------");
          for (int i = 0; i < maxIndex; i++)
          {
              Console.WriteLine("Page: " + Convert.ToChar(i+65) + " | inbound: " + GetInbound(i) + " | outbound: " + GetOutbound(i) + " | PR: " + PageRanks[i]);
          }
          
      }

      public void CalcPR(int itterations, float d)
      {
          for (int i = 0; i < itterations; i++)
          {
              for (int k = 0; k < maxIndex; k++)
              {
                  PageRanks[k] = InPR(k, d);
              }
          }
      }

      public float InPR(int index, float d)
      {
          float PR = PageRanks[index];
          PR = (1 - d);
          float temp = 0f;
          for (int i = 0; i < maxIndex; i++)
          {
              if (graphMatrix[i, index] > 0)
              {
                  float outTemp = GetOutbound(i);
                  temp += (PageRanks[i] / outTemp);
              }
          }

          temp = temp * d;
          PR += temp;
          return PR;
      }

      public int GetInbound(int index)
      {
          int count = 0;
          for (int i = 0; i < maxIndex; i++)
          {
              if (graphMatrix[i, index] > 0)
              {
                  count += 1;
              }
          }

          return count;
      }

      public int GetOutbound(int index)
      {
          int count = 0;
          for (int i = 0; i < maxIndex; i++)
          {
              if (graphMatrix[index, i] > 0)
              {
                  count += 1;
              }
          }

          return count;
      }
  }

  static void Main(string[] args)
  {

      Console.WriteLine("Use example? (Y)");
      string input = Console.ReadLine();
      if (input[0] == 'Y')
      {
          PageLink g = new PageLink(4);
          g.Show();
          int[,] temp;
          temp = new int[4, 4]
          {
              {0, 1, 0, 0},
              {1, 0, 1, 1},
              {0, 0, 0, 0},
              {1, 0, 0, 0}
          };
          g.PagePreSet(temp);
          Console.Clear();
      
          Console.WriteLine("Enter positive integer of iterations to make:");
          int iterations = Convert.ToInt32(Console.ReadLine());
      
          g.Show();
          g.Summary(iterations, 0.85f);
      }
      else
      {    
          Console.WriteLine("Enter positive integer (<=63) for number of pages:");
          string size = Console.ReadLine();
          PageLink g = new PageLink(Convert.ToInt32(size));
          g.PagesSetUp();
          Console.Clear();
      
          Console.WriteLine("Enter positive integer of iterations to make:");
          int iterations = Convert.ToInt32(Console.ReadLine());
      
          g.Show();
          g.Summary(iterations, 0.85f);
      }
      
  }
  
}