import { Injectable } from '@angular/core';

@Injectable()
export class ImageService {
    public loadImage(input: any, assign: Function, width?: number, height?: number): void {

        const reader = new FileReader();
        const image = document.createElement('img');

        reader.addEventListener('load', (event: any) => {
            assign(event.target.result);
        }, false);

        reader.readAsDataURL(input.files[0]);
    }

    public resize(imageData: string, width: number, height: number): string {

        const image = new Image();
        image.src = imageData;

        const canvas = document.createElement('canvas');
        canvas.width = width;
        canvas.height = height;

        const context = canvas.getContext('2d');
        context.drawImage(image, 0, 0, width, height);

        const result = canvas.toDataURL('image/jpeg');

        return result;
    }
}
