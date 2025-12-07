#include <string>
#include <iostream>
#include <algorithm>
using namespace std;
int main () {
	string a,b,c;
	cin >> a >> b;
	reverse (a.begin(),a.end());
	reverse (b.begin(),b.end());
	int bigger = max(a.size(),b.size());
	int smaller = min(a.size(),b.size());
	for (int i = smaller; i < bigger;i++) {
		if (a.size() > b.size()) b += '0';
		if (b.size() > a.size()) a += '0';
	} 
	for (int i = 0 ; i < bigger;i++) {
		a[i] -= '0';
		b[i] -= '0';
		c += static_cast<char>(a[i] + b[i]);
	}
	for (int i = 0; i < c.size();i++) {
		if (i == c.size() - 1 && c[i] / 10 != 0) {
			c += static_cast<int>(c[i] / 10);
			c[i] = c[i] % 10;
		}
		else if (i == c.size() - 1 && c[i] / 10 == 0) break;
		else {
			c[i + 1] += static_cast<char>(c[i] / 10);
			c[i] = c[i] % 10;
		}
	}
	reverse (c.begin(),c.end());
	for (int i = 0; i < c.size(); i++) c[i] += '0';
	cout << c;
}