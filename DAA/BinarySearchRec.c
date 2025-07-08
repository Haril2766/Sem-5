#include <stdio.h>

int binarySearch(int a[], int left, int right, int target) {
    if (left <= right) {
        int mean = (left + right) / 2;

        if (a[mean] == target)
            return mean;
        else if (a[mean] < target)
            return binarySearch(a, mean + 1, right, target);
        else
            return binarySearch(a, left, mean - 1, target);
    }
    return -1; // Element not found
}

void main() {
    int a[] = {1, 9, 11, 18, 19, 22, 25, 29, 36};
    int n = sizeof(a) / sizeof(a[0]);
    int target = 22;

    int result = binarySearch(a, 0, n - 1, target);

    if (result != -1) {
        printf("Element %d found at index %d\n", target, result);
    } else {
        printf("Element %d not found in the array\n", target);
    }
}
