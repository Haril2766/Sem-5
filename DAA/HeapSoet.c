#include<stdio.h>
#include<time.h>
#define N 10000

int heapify(int arr[],int n,int i)
{
    int largest = i;
    int right = i*2+2;
    int left = i*2+1;
    
    if(left < n && arr[left] >arr[largest])
    {
        left = largest;
    }
    if(right < n && arr[right] >arr[largest])
    {
        right = largest;
    }

    if(largest != i){
        int temp = arr[i];
        arr[i] = arr[largest];
        arr[largest] = temp;
        heapify(arr[], n,largest);
    }
}

int heapsort(int arr[],int n)
{
    for(int i = n/2; i>=0; i--)
    {
        heapify(arr,n,i)
    }
    for(int i = n-1;i>=0;i--){
        
    }
}