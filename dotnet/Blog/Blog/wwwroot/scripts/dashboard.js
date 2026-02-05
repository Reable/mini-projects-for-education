document.addEventListener('DOMContentLoaded', async () => {
    const recordsList = document.getElementById('records-list');
    const newRecordForm = document.getElementById('new-record-form');

    
    const userId = localStorage.getItem('id');
    document.getElementById('user-id').value = userId;

    async function init() {
        if(localStorage.getItem('token')) {
            const authBtn = document.getElementById('auth-button')
            authBtn.innerText = "Личный кабинет";
            authBtn.href = "/dashboard";
        }
        
        await loadRecords();
    }

    async function loadRecords() {
        try {
            
            const response = await fetch('/records/user/'+userId,{
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + localStorage.getItem('token')
                }
            });
            if (!response.ok) throw new Error('Ошибка загрузки записей');

            const data = await response.json();
            renderRecords(data);
        } catch (error) {
            alert('Не удалось загрузить записи: ' + error.message);
        }
    }

    // Отображение записей
    function renderRecords(records) {
        recordsList.innerHTML = '';
        if (records.length === 0) {
            recordsList.innerHTML = '<p>Записей пока нет.</p>';
            return;
        }

        records.forEach(record => {
            const item = document.createElement('div');
            item.className = 'record-item';
            item.dataset.id = record.id; // Сохраняем ID записи

            item.innerHTML = `
                <div class="record-header">
                    <div class="record-title">${record.title}</div>
                    <div class="record-actions">
                        <button class="btn-edit" data-id="${record.id}">Изменить</button>
                        <button class="btn-delete" data-id="${record.id}">Удалить</button>
                    </div>
                </div>
                <div class="record-content">${record.content}</div>
            `;

            item.querySelector('.btn-edit').addEventListener('click', () => openEditForm(record));
            item.querySelector('.btn-delete').addEventListener('click', () => deleteRecord(record.id));

            recordsList.appendChild(item);
        });
    }

    // Создание новой записи
    newRecordForm.addEventListener('submit', async (e) => {
        e.preventDefault();

        const title = document.getElementById('title').value;
        const content = document.getElementById('content').value;

        try {
            const response = await fetch('/records/create', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.getItem('token')}`,
                },
                body: JSON.stringify({ userId, title, content })
            });

            if (!response.ok) {
                const error = await response.json();
                alert('Ошибка: ' + (error.message || 'Не удалось создать запись'));
                return;
            }

            await loadRecords(); // Перезагружаем список
            newRecordForm.reset();
        } catch (error) {
            alert('Ошибка сети: ' + error.message);
        }
    });

    // Открытие формы редактирования
    function openEditForm(record) {
        // Заменяем форму создания на форму редактирования
        newRecordForm.innerHTML = `
            <h2>Редактировать запись</h2>
            <input type="hidden" id="edit-record-id" value="${record.id}">
            <label>Заголовок<input type="text" id="edit-title" value="${record.title}" required minlength="5"></label>
            <label>Текст<textarea id="edit-content" required minlength="10">${record.content}</textarea></label>
            <button type="button" id="save-edit">Сохранить</button>
            <button type="button" id="cancel-edit">Отмена</button>
        `;

        // Обработчики для кнопок редактирования
        document.getElementById('save-edit').addEventListener('click', saveEdit);
        document.getElementById('cancel-edit').addEventListener('click', () => {
            newRecordForm.innerHTML = ` <!-- Возвращаем исходную форму -->
                <h2>Создать запись</h2>
                <input type="hidden" id="user-id" value="${userId}">
                <label>Заголовок<input type="text" id="title" required minlength="5"></label>
                <label>Текст<textarea id="content" required minlength="10"></textarea></label>
                <button type="submit">Сохранить</button>
            `;
            newRecordForm.addEventListener('submit', async (e) => { /* ... */ });
        });
    }

    // Сохранение изменений
    async function saveEdit() {
        const recordId = parseInt(document.getElementById('edit-record-id').value);
        const title = document.getElementById('edit-title').value;
        const content = document.getElementById('edit-content').value;


        try {
            const response = await fetch('/records/update', {
                method: 'PUT',
                headers: { 
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + localStorage.getItem('token')
                },
                body: JSON.stringify({ recordId, userId, title, content })
            });

            if (!response.ok) {
                const error = await response.json();
                alert('Ошибка: ' + (error.message || 'Не удалось обновить запись'));
                return;
            }

            await loadRecords();
            newRecordForm.innerHTML = `
                <input type="hidden" id="user-id" value="${userId}">
                <label>Заголовок<input type="text" id="title" required minlength="5"></label>
                <label>Текст<textarea id="content" required minlength="10"></textarea></label>
                <button type="submit">Сохранить</button>
            `;
            newRecordForm.addEventListener('submit', async (e) => { /* ... */ });
        } catch (error) {
            alert('Ошибка сети: ' + error.message);
        }
    }

    // Удаление записи
     async function deleteRecord(recordId) {
         if (!confirm('Вы уверены, что хотите удалить эту запись?')) return;
    
         try {
             const response = await fetch(`/records/delete/${recordId}`, {
                 method: 'DELETE',
                 headers: {
                     'Content-Type': 'application/json',
                     'Authorization': 'Bearer ' + localStorage.getItem('token')
                 }
             });
    
             if (!response.ok) {
                 const error = await response.json();
                 alert('Ошибка: ' + (error.message || 'Не удалось удалить запись'));
                 return;
             }
    
             await loadRecords(); // Перезагружаем список
         } catch (error) {
             alert('Ошибка сети: ' + error.message);
         }
     }

    await init();
});
