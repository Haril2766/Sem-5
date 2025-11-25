#include <stdio.h>
#include <time.h>

int binarySearch(int arr[], int l, int r, int x) {
    if (r >= l) {
        int mid = l + (r - l) / 2;

        if (arr[mid] == x)
            return mid;

        if (arr[mid] > x)
            return binarySearch(arr, l, mid - 1, x);

        return binarySearch(arr, mid + 1, r, x);
    }

    return -1;
}

void printArray(int arr[], int len){
    for(int i = 0; i < len; i++){
        printf("%d\t", arr[i]);
    }
    printf("\n");
}

void main() {
    FILE *fp;
    clock_t start, end;
    int arr[100000];
    int n, userChoice, x;

    printf("1.best case\n2.worst case\n3.average case\n");
    scanf("%d", &userChoice);

    switch (userChoice) {
        case 1:
            fp = fopen("best.txt", "r");
            break;
        case 2:
            fp = fopen("worst.txt", "r");
            break;
        case 3:
            fp = fopen("average.txt", "r");
            break;
        default:
            printf("Enter valid number!!!\n");
            return;
    }
    
    printf("Enter number of elements: ");
    scanf("%d", &n);

    for (int i = 0; i < n; i++) {
        fscanf(fp, "%d", &arr[i]);
    }

    fclose(fp);

    printf("Enter element to search: ");
    scanf("%d", &x);

    start = clock();
    int result = binarySearch(arr, 0, n - 1, x);
    end = clock();

    printArray(arr, n);

    if (result != -1) {
        printf("Element found at index %d\n", result);
    } else {
        printf("Element not found in the array\n");
    }

    double cpuTime = ((double)(end - start)) / CLOCKS_PER_SEC;
    printf("Time taken by Binary Search: %f seconds\n", cpuTime);
}
