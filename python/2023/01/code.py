import os
sd = os.path.dirname(os.path.abspath(__file__))

def getworddigit(l, i):
  if(i >= 2):
    if(l[i-2:i+1] == "one"):  return 1
    if(l[i-2:i+1] == "two"):  return 2 
    if(l[i-2:i+1] == "six"):  return 6
  if(i >= 3):
    if(l[i-3:i+1] == "four"):  return 4
    if(l[i-3:i+1] == "five"):  return 5 
    if(l[i-3:i+1] == "nine"):  return 9
  if(i >= 4):
    if(l[i-4:i+1] == "three"):  return 3
    if(l[i-4:i+1] == "seven"):  return 7 
    if(l[i-4:i+1] == "eight"):  return 8
  return -1

def addLine(l, allow_word_digits = False):
  result = 0
  length = len(l)
  for i in range(0, length):
    ch = l[i]
    if(ch.isdigit()): 
       result += 10*(ord(ch)-ord('0'))
       break
    if(allow_word_digits):
      val = getworddigit(l, i)
      if(val >= 0):
        result += 10*val
        break
  for i in range(length - 1, -1, -1):
    ch = l[i]
    if(ch.isdigit()): 
       result += ord(ch)-ord('0')
       break
    if(allow_word_digits):
      val = getworddigit(l, i)
      if(val >= 0):
        result += val
        break
  return result

def evaluatefile(filepath, allow_word_digits = False):
  result = 0
  with open(filepath) as file:
    for line in file:
      result += addLine(line, allow_word_digits)
  return result
    
print(evaluatefile(sd+"/example1.txt"))
print(evaluatefile(sd+"/input1.txt"))
print(evaluatefile(sd+"/example2.txt", False))
print(evaluatefile(sd+"/example2.txt", True))
print(evaluatefile(sd+"/input1.txt", True))