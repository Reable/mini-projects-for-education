document.addEventListener('DOMContentLoaded', () => {
    const tabButtons = document.querySelectorAll('.tab-button');
    const forms = document.querySelectorAll('.auth-form');

    tabButtons.forEach(button => {
        button.addEventListener('click', () => {
            tabButtons.forEach(btn => btn.classList.remove('active'));
            forms.forEach(form => form.classList.remove('active'));

            button.classList.add('active');
            const tabName = button.getAttribute('data-tab');
            document.getElementById(`${tabName}-form`).classList.add('active');
        });
    });

    document.getElementById('login-form').addEventListener('submit', async (e) => {
        e.preventDefault();

        const login = document.getElementById('login-email').value;
        const password = document.getElementById('login-password').value;
        const errorEl = document.getElementById('login-error');

        errorEl.textContent = '';
        errorEl.style.display = 'none';

        try {
            const response = await fetch('/users/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ login, password })
            });

            if (response.ok) {
                localStorage.setItem("token", await response.json());
                alert('Вход выполнен успешно!');
                window.location.href = '/dashboard';
                return;
            }

            if (response.status === 401) {
                const errorData = await response.json();
                errorEl.textContent = errorData.detail;
                errorEl.style.display = 'block';
                return;
            }

            if (response.status === 500) {
                errorEl.textContent = 'Серверная ошибка. Попробуйте позже.';
                errorEl.style.display = 'block';
                return;
            }

            errorEl.textContent = `Ошибка: ${response.status} ${response.statusText}`;
            errorEl.style.display = 'block';

        } catch (error) {
            errorEl.textContent = 'Не удалось подключиться к серверу. Проверьте интернет-соединение.';
            errorEl.style.display = 'block';
        }
    });


    document.getElementById('register-form').addEventListener('submit', async (e) => {
        e.preventDefault();

        const login = document.getElementById('register-login').value;
        const password = document.getElementById('register-password').value;
        const confirmedPassword = document.getElementById('register-confirm').value;
        const errorEl = document.getElementById('register-error');

        errorEl.textContent = '';

        if (!login.trim()) {
            errorEl.textContent = 'Пожалуйста, укажите логин.';
            return;
        }

        if (password.length < 10) {
            errorEl.textContent = 'Пароль должен быть не менее 6 символов.';
            return;
        }

        if (password !== confirmedPassword) {
            errorEl.textContent = 'Пароли не совпадают.';
            return;
        }

        try {
            const response = await fetch('/users/register', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ login, password, confirmedPassword })
            });

            if (response.ok) {
                alert('Регистрация успешна! Теперь можно войти.');
                document.querySelector('.tab-button[data-tab="login"]').click();
                return;
            }

            if (response.status === 401) {
                const errorData = await response.json();
                errorEl.textContent = errorData.detail;
                errorEl.style.display = 'block';
                return;
            }

            if (response.status === 500) {
                errorEl.textContent = 'Серверная ошибка. Попробуйте позже.';
                errorEl.style.display = 'block';
            }
        } catch (error) {
            errorEl.textContent = 'Ошибка регистрации. Попробуйте ещё раз.';
        }
    });

    tabButtons.forEach(button => {
        button.addEventListener('click', () => {
            document.getElementById('login-form').reset();
            document.getElementById('register-form').reset();
            document.getElementById('login-error').textContent = '';
            document.getElementById('register-error').textContent = '';
        });
    });
});
