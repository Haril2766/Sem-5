#include <stdio.h>
#include <stdlib.h>

int *queue;
int f = -1, r = -1;
int size;

// Enqueue (insert element)
void Enqueue(int value) {
    if (r == size - 1) {
        printf("Queue Overflow!\n");
    } else {
        if (f == -1) f = 0;
        r++;
        queue[r] = value;
        printf("%d enqueued to queue.\n", value);
    }
}
