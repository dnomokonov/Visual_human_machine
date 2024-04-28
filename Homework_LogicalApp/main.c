#include <iostream>
#include <vector>
#include <string>
#include <algorithm>

using namespace std;

string sortString(string str)
{
sort(str.begin(), str.end());
return str;
}

int binLeft(int start, int end, bool (*func)(int))
{
int left = start, right = end;

while (left <= right)
{
int mid = (left + right) / 2;

if (func(mid))
{
if (!func(mid + 1))
{
return mid;
}
else
{
left = mid + 1;
}
}
else
{
right = mid - 1;
}
}
return -1; // Indicating failure
}

int binRight(int start, int end, bool (*func)(int))
{
int left = start, right = end;

while (left <= right)
{
int mid = (left + right) / 2;

if (func(mid))
{
if (!func(mid - 1))
{
return mid;
}
else
{
right = mid - 1;
}
}
else
{
left = mid + 1;
}
}
return -1; // Indicating failure
}

bool solve()
{
int n, m;
cin >> n >> m;
vector<string> arr;

bool topLeft = false, topRight = false, bottomLeft = false, bottomRight = false;
bool topW = false, botW = false, leftW = false, rightW = false;
bool topB = false, botB = false, leftB = false, rightB = false;

for (int i = 0; i < n; ++i)
{
string s;
cin >> s;
arr.push_back(s);
if (i == 0)
{
topLeft = (s[0] == 'B');
topRight = (s[s.size() - 1] == 'B');

if (s.find('W') != string::npos)
topW = true;
if (s.find('B') != string::npos)
topB = true;
}
if (i == n - 1)
{
bottomLeft = (s[0] == 'B');
bottomRight = (s[s.size() - 1] == 'B');

if (s.find('W') != string::npos)
botW = true;
if (s.find('B') != string::npos)
botB = true;
}

if (s[0] == 'W')
leftW = true;
if (s[0] == 'B')
leftB = true;

if (s[s.size() - 1] == 'W')
rightW = true;
if (s[s.size() - 1] == 'B')
rightB = true;
}

if (topRight == bottomLeft || topLeft == bottomRight)
{
cout << "YES" << endl;
return true;
}
if (n == 1 || m == 1)
{
cout << "NO" << endl;
return false;
}
if (topRight && topW && rightW)
{
cout << "YES" << endl;
return true;
}
if (!topRight && topB && rightB)
{
cout << "YES" << endl;
return true;
}
if (topLeft && topW && leftW)
{
cout << "YES" << endl;
return true;
}
if (!topLeft && topB && leftB)
{
cout << "YES" << endl;
return true;
}
if (bottomLeft && botW && leftW)
{
cout << "YES" << endl;
return true;
}
if (!bottomLeft && botB && leftB)
{
cout << "YES" << endl;
return true;
}
if (bottomRight && botW && rightW)
{
cout << "YES" << endl;
return true;
}
if (!bottomRight && botB && rightB)
{
cout << "YES" << endl;
return true;
}

cout << "NO" << endl;
return false;
}

int main()
{
int t;
cin >> t;
for (int lamoda = 0; lamoda < t; ++lamoda)
solve();
return 0;
}