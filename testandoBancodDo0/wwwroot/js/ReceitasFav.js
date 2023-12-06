// JavaScript para a página de receita 1
const imageContainers1 = document.querySelectorAll('#recipe1 .imageContainer');
imageContainers1.forEach(container => {
    container.addEventListener('click', event => {
        const icon = event.currentTarget.querySelector('.favoriteIcon');
        icon.classList.toggle('favorited');

        const imageSrc = event.currentTarget.querySelector('.image').src;

        if (icon.classList.contains('favorited')) {
            localStorage.setItem('favoritedImage1', imageSrc);
            icon.style.color = '#f44336'; // Muda a cor para vermelho quando favoritado
        } else {
            localStorage.removeItem('favoritedImage1');
            icon.style.color = '#fe9900'; // Mantém a cor amarela quando desfavoritado
        }
    });
});

// JavaScript para a página de receita 2
const imageContainers2 = document.querySelectorAll('#recipe2 .imageCont');
imageContainers2.forEach(container => {
    container.addEventListener('click', event => {
        const icon = event.currentTarget.querySelector('.favorite');
        icon.classList.toggle('favorited');

        const imageSrc = event.currentTarget.querySelector('.image').src;

        if (icon.classList.contains('favorited')) {
            localStorage.setItem('favoritedImage2', imageSrc);
            icon.style.color = '#f44336'; // Muda a cor para vermelho quando favoritado
        } else {
            localStorage.removeItem('favoritedImage2');
            icon.style.color = '#fe9900'; // Mantém a cor amarela quando desfavoritado
        }
    });
});
// JavaScript para a página de receita 3
const imageContainers3 = document.querySelectorAll('#recipe3 .imageCont1');
imageContainers3.forEach(container => {
    container.addEventListener('click', event => {
        const icon = event.currentTarget.querySelector('.favorite1');
        icon.classList.toggle('favorited');

        const imageSrc = event.currentTarget.querySelector('.image').src;

        if (icon.classList.contains('favorited')) {
            localStorage.setItem('favoritedImage3', imageSrc);
            icon.style.color = '#f44336'; // Muda a cor para vermelho quando favoritado
        } else {
            localStorage.removeItem('favoritedImage3');
            icon.style.color = '#fe9900'; // Mantém a cor amarela quando desfavoritado
        }
    });
});
// JavaScript para a página de receita 4
const imageContainers4 = document.querySelectorAll('#recipe4 .imageCont2');
imageContainers4.forEach(container => {
    container.addEventListener('click', event => {
        const icon = event.currentTarget.querySelector('.favorite2');
        icon.classList.toggle('favorited');

        const imageSrc = event.currentTarget.querySelector('.image').src;

        if (icon.classList.contains('favorited')) {
            localStorage.setItem('favoritedImage4', imageSrc);
            icon.style.color = '#f44336'; // Muda a cor para vermelho quando favoritado
        } else {
            localStorage.removeItem('favoritedImage4');
            icon.style.color = '#fe9900'; // Mantém a cor amarela quando desfavoritado
        }
    });
});
// JavaScript para a página de receita 5
const imageContainers5 = document.querySelectorAll('#recipe5 .imageCont3');
imageContainers5.forEach(container => {
    container.addEventListener('click', event => {
        const icon = event.currentTarget.querySelector('.favorite3');
        icon.classList.toggle('favorited');

        const imageSrc = event.currentTarget.querySelector('.image').src;

        if (icon.classList.contains('favorited')) {
            localStorage.setItem('favoritedImage5', imageSrc);
            icon.style.color = '#f44336'; // Muda a cor para vermelho quando favoritado
        } else {
            localStorage.removeItem('favoritedImage5');
            icon.style.color = '#fe9900'; // Mantém a cor amarela quando desfavoritado
        }
    });
});
// JavaScript para a página de receita 6
const imageContainers6 = document.querySelectorAll('#recipe6 .imageCont4');
imageContainers6.forEach(container => {
    container.addEventListener('click', event => {
        const icon = event.currentTarget.querySelector('.favorite4');
        icon.classList.toggle('favorited');

        const imageSrc = event.currentTarget.querySelector('.image').src;

        if (icon.classList.contains('favorited')) {
            localStorage.setItem('favoritedImage6', imageSrc);
            icon.style.color = '#f44336'; // Muda a cor para vermelho quando favoritado
        } else {
            localStorage.removeItem('favoritedImage6');
            icon.style.color = '#fe9900'; // Mantém a cor amarela quando desfavoritado
        }
    });
});

document.addEventListener('DOMContentLoaded', () => {
    const favoritedImageContainer1 = document.getElementById('favoritedImageContainer1');
    const favoritedImageContainer2 = document.getElementById('favoritedImageContainer2');
    const favoritedImageContainer3 = document.getElementById('favoritedImageContainer3');
    const favoritedImageContainer4 = document.getElementById('favoritedImageContainer4');
    const favoritedImageContainer5 = document.getElementById('favoritedImageContainer5');
    const favoritedImageContainer6 = document.getElementById('favoritedImageContainer6');
    const card1 = document.getElementById('card1');
    const card2 = document.getElementById('card2');
    const card3 = document.getElementById('card3');
    const card4 = document.getElementById('card4');
    const card5 = document.getElementById('card5');
    const card6 = document.getElementById('card6');

    const favoritedImageSrc1 = localStorage.getItem('favoritedImage1');
    const favoritedImageSrc2 = localStorage.getItem('favoritedImage2');
    const favoritedImageSrc3 = localStorage.getItem('favoritedImage3');
    const favoritedImageSrc4 = localStorage.getItem('favoritedImage4');
    const favoritedImageSrc5 = localStorage.getItem('favoritedImage5');
    const favoritedImageSrc6 = localStorage.getItem('favoritedImage6');

    if (favoritedImageSrc1) {
        const image1 = document.createElement('img');
        image1.src = favoritedImageSrc1;
        image1.alt = 'Imagem Favoritada 1';
        image1.classList.add('card-img-top');
        favoritedImageContainer1.appendChild(image1);
        card1.style.display = 'block'; // Mostra o card se houver uma imagem favoritada

        const removeButton1 = document.querySelector('#card1 .btn.btn-primary:nth-of-type(2)');
        removeButton1.addEventListener('click', () => {
            localStorage.removeItem('favoritedImage1');
            favoritedImageContainer1.removeChild(image1);
            card1.style.display = 'none';
        });
    }
    if (favoritedImageSrc2) {
        const image2 = document.createElement('img');
        image2.src = favoritedImageSrc2;
        image2.alt = 'Imagem Favoritada 2';
        image2.classList.add('card-img-top');
        favoritedImageContainer2.appendChild(image2);
        card2.style.display = 'block'; // Mostra o card se houver uma imagem favoritada

        const removeButton2 = document.querySelector('#card2 .btn.btn-primary:nth-of-type(2)');
        removeButton2.addEventListener('click', () => {
            localStorage.removeItem('favoritedImage2');
            favoritedImageContainer2.removeChild(image2);
            card2.style.display = 'none';
        });
    }
    if (favoritedImageSrc3) {
        const image3 = document.createElement('img');
        image3.src = favoritedImageSrc3;
        image3.alt = 'Imagem Favoritada 3';
        image3.classList.add('card-img-top');
        favoritedImageContainer3.appendChild(image3);
        card3.style.display = 'block'; // Mostra o card se houver uma imagem favoritada

        const removeButton3 = document.querySelector('#card3 .btn.btn-primary:nth-of-type(2)');
        removeButton3.addEventListener('click', () => {
            localStorage.removeItem('favoritedImage3');
            favoritedImageContainer3.removeChild(image3);
            card3.style.display = 'none';
        });
    }
    if (favoritedImageSrc4) {
        const image4 = document.createElement('img');
        image4.src = favoritedImageSrc4;
        image4.alt = 'Imagem Favoritada 4';
        image4.classList.add('card-img-top');
        favoritedImageContainer4.appendChild(image4);
        card4.style.display = 'block'; // Mostra o card se houver uma imagem favoritada

        const removeButton4 = document.querySelector('#card4 .btn.btn-primary:nth-of-type(2)');
        removeButton4.addEventListener('click', () => {
            localStorage.removeItem('favoritedImage4');
            favoritedImageContainer4.removeChild(image4);
            card4.style.display = 'none';
        });
    }
    if (favoritedImageSrc5) {
        const image5 = document.createElement('img');
        image5.src = favoritedImageSrc5;
        image5.alt = 'Imagem Favoritada 5';
        image5.classList.add('card-img-top');
        favoritedImageContainer5.appendChild(image5);
        card5.style.display = 'block'; // Mostra o card se houver uma imagem favoritada

        const removeButton5 = document.querySelector('#card5 .btn.btn-primary:nth-of-type(2)');
        removeButton5.addEventListener('click', () => {
            localStorage.removeItem('favoritedImage5');
            favoritedImageContainer5.removeChild(image5);
            card5.style.display = 'none';
        });
    }
    if (favoritedImageSrc6) {
        const image6 = document.createElement('img');
        image6.src = favoritedImageSrc6;
        image6.alt = 'Imagem Favoritada 6';
        image6.classList.add('card-img-top');
        favoritedImageContainer6.appendChild(image6);
        card6.style.display = 'block'; // Mostra o card se houver uma imagem favoritada

        const removeButton6 = document.querySelector('#card6 .btn.btn-primary:nth-of-type(2)');
        removeButton6.addEventListener('click', () => {
            localStorage.removeItem('favoritedImage6');
            favoritedImageContainer6.removeChild(image6);
            card6.style.display = 'none';
        });
    }
});
