window.onload = loadRecords;

async function loadRecords() {
    const records = await fetch("/records")
    const data = await records.json()
    console.log(data)

    const container = document.getElementById('posts-container');
    data.forEach(post => {
        container.appendChild(createPostCard(post));
    });
}

function createPostCard(post) {
    const link = document.createElement("a");
    link.classList.add(`post-link-${post.id}`);

    const card = document.createElement('div');
    link.appendChild(card);
    card.className = 'post-card';

    card.innerHTML = `
        <div class="post-header">${post.title}</div>
        <div class="post-content">
            <p>${post.content}</p>
            <div class="post-meta">Автор: пользователь #${post.userId} (ID поста: ${post.id})</div>
        </div>
    `;
    
    link.addEventListener('click', async e => {
        await loadRecordById(post.id);
    })

    return link;
}


async function loadRecordById(id) {
    const records = await fetch("/records/" + id)
    const data = await records.json()
    console.log(data)
}