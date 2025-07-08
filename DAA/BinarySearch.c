#include<stdio.h>
void main(){
    int a[] = {1,9,11,18,19,22,25,29,36};
    int n = sizeof(a)/sizeof(a[0]);
    int target = 29;
    int found = 0;
    int left = 0;
    int right = n-1;

    while(left<= right){
        int mean = (left + right)/2;

        if(a[mean] == target){
            printf("Element %d is found at %d",target,mean);
            found = 1;
            break;
        }
        else if(a[mean] < target){
            left = mean + 1;
        }
        else{
            right = mean -1;
        }
    }
    if(!found){
        printf("Element Is not Found");
    }
}