#include<stdio.h>
void main(){
    int a[] = {3,6,8,68,9,75,39,26,7};
    int n = sizeof(a)/sizeof(a[0]);
    int target = 90;
    int found = 0;
    
    for(int i = 0; i < n;i++){
        if(a[i] == target){
            printf("Element %d is found at %d",target,i);
            found = 1;
            break;
        }
    }
    if(!found){
        printf("Element Is not Found");
    }
}

