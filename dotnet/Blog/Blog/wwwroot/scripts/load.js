window.onload = init;

async function init() {
    if(localStorage.getItem('token')) {
        const authBtn = document.getElementById('auth-button')
        authBtn.innerText = "Личный кабинет";
        authBtn.href = "/dashboard";
    }
    await loadRecords()
}

async function loadRecords() {
    const records = await fetch("/records");
    const data = await records.json();

    const container = document.getElementById('posts-container');
    container.innerHTML = ''; 

    data.forEach(post => {
        const card = createPostCard(post);
        container.appendChild(card);
    });

    initPopup();
}

function createPostCard(post) {
    const card = document.createElement('div');
    card.className = 'post-card';
    card.dataset.postId = post.id;

    // Обрезаем текст до 100 символов + добавляем ... если нужно
    const maxLength = 100;
    const truncatedContent = post.content.length > maxLength
        ? post.content.substring(0, maxLength) + '...'
        : post.content;

    card.innerHTML = `
        <div class="post-header">${post.title}</div>
        <div class="post-content">
            <p>${truncatedContent}</p>
            <div class="post-meta">Автор: пользователь #${post.userId}</div>
        </div>
    `;

    card.addEventListener('click', () => {
        showPostPopup(post);
    });

    return card;
}


function initPopup() {
    const overlay = document.getElementById('popup-overlay');
    const closeBtn = document.getElementById('close-popup');

    closeBtn.addEventListener('click', () => {
        overlay.classList.remove('active');
    });

    overlay.addEventListener('click', (e) => {
        if (e.target === overlay) {
            overlay.classList.remove('active');
        }
    });
}

function showPostPopup(post) {
    const overlay = document.getElementById('popup-overlay');
    const titleEl = document.getElementById('popup-title');
    const contentEl = document.getElementById('popup-content-text');
    const metaEl = document.getElementById('popup-meta');

    titleEl.textContent = post.title;
    contentEl.innerHTML = `<p>${post.content}</p>`;
    metaEl.textContent = `Автор: пользователь #${post.userId} (ID поста: ${post.id})`;

    overlay.classList.add('active');
}
