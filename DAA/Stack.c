#include <stdio.h>
#include <stdlib.h>

int *s;
int top = -1;
int size;

// Push function
void push(int value) {
    if (top == size - 1) {
        printf("Stack Overflow!\n");
    } else {
        top++;
        s[top] = value;
        printf("%d pushed to stack.\n", value);
    }
}

// Pop function
void pop() {
    if (top == -1) {
        printf("Stack Underflow!\n");
    } else {
        printf("%d popped from stack.\n", s[top]);
        top--;
    }
}

// Display function
void display() {
    if (top == -1) {
        printf("Stack is empty.\n");
    } else {
        printf("Stack elements: ");
        for (int i = top; i >= 0; i--) {
            printf("%d ", s[i]);
        }
        printf("\n");
    }
}

int main() {
    int choice, value;

    printf("Enter stack size: ");
    scanf("%d", &size);

    s = (int *)malloc(size * sizeof(int));

    if (s == NULL) {
        printf("allocation failed!\n");
        return 1;
    }

    while (1) {
        
        printf("1 for -> Push\n2 for -> Pop\n3 for -> Display\n");
        printf("Enter your choice: ");
        scanf("%d", &choice);

        switch (choice) {
            case 1:
                printf("Enter value: ");
                scanf("%d", &value);
                push(value);
                break;
            case 2:
                pop();
                break;
            case 3:
                display();
                break;
            default:
                printf("Invalid choice! Try again.\n");
        }
    }

    return 0;
}
